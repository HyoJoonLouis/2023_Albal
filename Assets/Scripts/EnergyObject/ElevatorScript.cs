using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ElevatorScript : EnergyObject
{
    [SerializeField]
    float Speed;

    [SerializeField] Transform StartPosition;
    [SerializeField] Transform EndPosition;

    IEnumerator coroutine;

    float deltaTime = 0;

    public GameObject PlayerCharacter;


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

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player Enter" + collision.transform.tag);
        Debug.Log(collision.transform.name);
        PlayerCharacter = collision.gameObject;
        PlayerCharacter.transform.SetParent(transform, true);
    }

    public void OnCollisionExit(Collision collision)
    {
        Debug.Log("Player Out");
        PlayerCharacter.transform.SetParent(null);
    }

}
