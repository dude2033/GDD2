using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneProgressionManager : Singleton<SceneProgressionManager>
{
    public UnityEvent onProgress;
    private int sceneIndex = 0;
    private bool isLoading, isUnloading;
    private AsyncOperation loadOP;

    [ContextMenu("Progress to next scene")]
    public void ProgressWithUnloading() => Progress();

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    public void Progress(bool suppressEvent = false, bool unloadLastScene = true)
    {
        // already reached the last scene
        if(sceneIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            return;
        
        if (unloadLastScene)
            UnloadLastScene();

        sceneIndex++;
        loadOP.allowSceneActivation = true;
        
        PreloadNextScene();
        
        if(!suppressEvent)
            onProgress.Invoke();
    }


    private void TryCompleteRoomsetup(AsyncOperation obj)
    {
        loadOP.completed -= TryCompleteRoomsetup;
        Scene currentScene = SceneManager.GetSceneByBuildIndex(sceneIndex);
        SceneManager.SetActiveScene(currentScene);
        
        // terrible code
        var roomSetups =  FindObjectsOfType<RoomSetup>();
        
        if(roomSetups.Length == 0)
            return;
        
        RoomSetup roomSetup = roomSetups[roomSetups.Length - 1];
        roomSetup.destroyObjects = true;
        roomSetup.setupOnStart = false;
        roomSetup.SetupRoom();
    }
    
    private void PreloadNextScene()
    {
        if ((sceneIndex + 1) < SceneManager.sceneCountInBuildSettings)
            StartCoroutine(PreloadScene(sceneIndex + 1));
    }
    
    IEnumerator PreloadScene(int sceneName)
    {
        isLoading = true;
 
        loadOP = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadOP.allowSceneActivation = false;
        loadOP.completed += TryCompleteRoomsetup;
        
        while (isLoading)
        {
            if (loadOP.isDone)
            {
                isLoading = false;
            }
            yield return 0;
        }
    }



    IEnumerator UnloadScene(int sceneName)
    {
        isUnloading = false;
        AsyncOperation op = SceneManager.UnloadSceneAsync(sceneName);
        while (isUnloading)
        {
            if (op.isDone)
                isUnloading = false;
            
            yield return 0;
        }
        yield return 0;
    }
    
    private void UnloadLastScene()
    {
        StartCoroutine(UnloadScene(sceneIndex));
    }

    protected override void OnApplicationQuitCallback()
    {
        loadOP.completed -= TryCompleteRoomsetup;
    }
    
    protected override void OnEnableCallback()
    {
    }
    
    IEnumerator StartGame()
    {
        PreloadNextScene();
        Progress(true, false);
        yield return null;
    }
}
