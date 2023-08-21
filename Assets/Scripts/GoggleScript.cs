using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoggleScript : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void GoggleOn()
    {
        animator.Play("GoggleOnAnimation");
    }

    public void GoggleOff()
    {
        animator.Play("GoggleOffAnimation");
    }
}
