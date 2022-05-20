using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneProgressionManager : Singleton<SceneProgressionManager>
{
    [SerializeField] private List<SceneAsset> sceneProgression;
    
    private int currentSceneIndex, lastLoadedSceneIndex;
    private bool isLoading, isUnloading, reachedEnd;
    private AsyncOperation loadOP;

    public UnityEvent onProgress;

    [ContextMenu("Progress to next scene")]
    public void ProgressWithUnloading() => Progress();
    
    public void Progress(bool suppressEvent = false, bool unloadLastScene = true)
    {
        if (unloadLastScene)
        {
            UnloadLastScene();
        }

        lastLoadedSceneIndex = currentSceneIndex - 1;
        loadOP.allowSceneActivation = true;
        
        PreloadNextScene();
        
        if(!suppressEvent)
            onProgress.Invoke();
    }


    private void TryCompleteRoomsetup(AsyncOperation obj)
    {
        loadOP.completed -= TryCompleteRoomsetup;
        Scene currentScene = SceneManager.GetSceneByName(sceneProgression[lastLoadedSceneIndex].name);
        SceneManager.SetActiveScene(currentScene);
        
        // terrible code
        var roomSetups =  FindObjectsOfType<RoomSetup>();
        
        if(roomSetups.Length == 0)
            return;
        
        RoomSetup roomSetup = roomSetups[roomSetups.Length - 1];
        roomSetup.SetupRoom();
    }
    
    private void PreloadNextScene()
    {
        if (currentSceneIndex < sceneProgression.Count)
            StartCoroutine(PreloadScene(sceneProgression[currentSceneIndex++].name));
    }
    
    IEnumerator PreloadScene(string sceneName)
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



    IEnumerator UnloadScene(string sceneName)
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
        if(reachedEnd)
            return;
        
        // -2 because we want to unload the scene before the active one
        // currentSceneIndex gets increased on preload
        StartCoroutine(UnloadScene(sceneProgression[currentSceneIndex - 2].name));
        
        if (currentSceneIndex == sceneProgression.Count)
            reachedEnd = true;
    }

    protected override void OnApplicationQuitCallback()
    {
        loadOP.completed -= TryCompleteRoomsetup;
    }

    IEnumerator StartGame()
    {
        PreloadNextScene();

        while (isLoading)
        {
            yield return 0;
        }
        Progress(true, false);
    }

    protected override void OnEnableCallback()
    {
        // Load the first scene and wait for completion
        StartCoroutine(StartGame());
    }
}
