using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(MeshRenderer))]
public class Dissolver : MonoBehaviour
{
    [SerializeField] private float dissolveDuration = 3f;
    [SerializeField] private float destroyTime = 3f;
    private BoxCollider collider;
    private Material mat;
    private static readonly int Dissolve = Shader.PropertyToID("dissolve");

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
    }

    [ContextMenu("dissolve")]
    public void StartDissolveProcess()
    {
        Destroy(gameObject, destroyTime);
        StartCoroutine(DissolveCoroutine());
    }

    IEnumerator DissolveCoroutine()
    {
        float currentTime = 0;
        while (currentTime < dissolveDuration)
        {
            currentTime += Time.deltaTime;
            mat.SetFloat(Dissolve, Mathf.Lerp(0,1,currentTime / dissolveDuration));
            yield return null;
        }
        mat.SetFloat(Dissolve, 1);
        yield return null;
    }
}
