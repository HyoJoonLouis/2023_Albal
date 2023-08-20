using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : EnergyObject
{
    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }


    public override void Activate()
    {
        animator.Play("Open");
    }
    public override void Deactivate()
    {
        animator.Play("Close");
    }
}
