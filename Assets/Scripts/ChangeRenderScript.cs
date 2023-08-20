using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRenderScript : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] float time;
    
    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();        
    }

    public void ChangeRender()
    {
        StartCoroutine("ChangeRenderCoroutine");
    }

    IEnumerator ChangeRenderCoroutine()
    {
        skinnedMeshRenderer.material.SetFloat("_Lerp", 1);
        yield return new WaitForSeconds(time);
        skinnedMeshRenderer.material.SetFloat("_Lerp", 0);
    }

}
