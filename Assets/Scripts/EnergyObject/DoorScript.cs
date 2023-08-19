using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : EnergyObject
{
    Animator animator;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }


    public override void Activate()
    {
        animator.Play("Open");
    }
}
