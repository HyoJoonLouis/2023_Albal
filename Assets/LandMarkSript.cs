using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkSript : MonoBehaviour
{
    Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;
        animator.Play("Open");
    }
}