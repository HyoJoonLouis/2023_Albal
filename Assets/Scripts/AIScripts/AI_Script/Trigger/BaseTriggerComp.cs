using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTriggerComp : MonoBehaviour
{
    [SerializeField] private bool IsRender = false;

    protected virtual void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = IsRender;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

    }
    protected virtual void OnTriggerExit(Collider other)
    {
    }
}
