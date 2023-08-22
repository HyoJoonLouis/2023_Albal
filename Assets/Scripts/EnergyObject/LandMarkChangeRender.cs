using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkChangeRender : ChangeRenderScript
{
    MeshRenderer[] meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
    }

    public override void ChangeRender()
    {
        StartCoroutine("ChangeRenderCoroutine");
    }

    IEnumerator ChangeRenderCoroutine()
    {
        for(int i = 0; i< meshRenderer.Length; i++)
        {
            meshRenderer[i].material.SetFloat("_Lerp", 1);
        }
        yield return new WaitForSeconds(time);

        for(int i = 0; i< meshRenderer.Length; i++)
        {
            meshRenderer[i].material.SetFloat("_Lerp", 0);
        }
    }
}
