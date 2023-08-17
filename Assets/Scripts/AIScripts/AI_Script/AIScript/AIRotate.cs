using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRotate : MonoBehaviour
{
    [SerializeField] private float RotateSpeed;
    private Transform TargetTransform;
    public Transform SetTargetTransform
    {
        set { TargetTransform = value; }
    }
    private void Start()
    {
        StopRotate();
    }

    private void Update()
    {
        Vector3 dir = TargetTransform.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * RotateSpeed);
    }

    public void StartRotate()
    {
        enabled = true;
    }

    public void StopRotate()
    {
        enabled = false;
    }
}
