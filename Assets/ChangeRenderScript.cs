using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRenderScript : MonoBehaviour
{
    Renderer renderer;
    
    void Awake()
    {
        renderer = GetComponent<Renderer>();        
    }

    public void ChangeRender()
    {
        StartCoroutine("ChangeRenderCoroutine");
    }

    IEnumerator ChangeRenderCoroutine()
    {
        renderer.material.SetFloat("_Lerp", 1);
        yield return new WaitForSeconds(0.5f);
        renderer.material.SetFloat("_Lerp", 0);
    }

}
