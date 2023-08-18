using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AIMovement : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent Agent;
    private Transform TargetTransform;

    public Transform SetTargetTransform
    {
        set { TargetTransform = value; }
    }

    void Start()
    {
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void StartMove()
    {
        enabled = true;
    }

    public void StopMove()
    {
        enabled = false;
        Agent.destination = transform.position;
    }

    private void MoveToPosition()
    {
        Vector3 Dir = (transform.position - TargetTransform.position).normalized;
        Agent.SetDestination(TargetTransform.position + Dir * 2.0f);
    }

    private void Update()
    {
        MoveToPosition();
    }
}