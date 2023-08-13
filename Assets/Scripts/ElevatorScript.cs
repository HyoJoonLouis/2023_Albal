using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : EnergyObject
{
    [SerializeField]
    float Speed;

    Transform StartPosition;
    Transform EndPosition;

    IEnumerator coroutine;

    float deltaTime = 0;

    void Start()
    {
        base.Start();
        StartPosition = transform.Find("StartPosition");
        EndPosition = transform.Find("EndPosition");
        transform.DetachChildren();
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;
    }

    public override void Activate()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
        coroutine = Move(EndPosition.position);
        StartCoroutine(coroutine);
    }

    public override void Deactivate()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
        coroutine = Move(StartPosition.position);
        StartCoroutine(coroutine);
    }

    IEnumerator Move(Vector3 TargetPosition)
    {
        while(Vector3.Distance(transform.position, TargetPosition) > 0.01f)
        { 
            this.transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * deltaTime);
            yield return null;
        }
    }

}
