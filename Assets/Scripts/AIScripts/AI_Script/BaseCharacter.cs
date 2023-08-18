using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacter : MonoBehaviour
{
    protected float DeltaTime;

    protected virtual void Awake()
    {
    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        DeltaTime += Time.deltaTime;
    }
}
