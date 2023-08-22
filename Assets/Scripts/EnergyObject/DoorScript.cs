using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : EnergyObject
{
    [SerializeField] RandomSounds<AudioClip> OpenSounds;
    [SerializeField] RandomSounds<AudioClip> CloseSounds;

    Animator animator;
    AudioSource audioSource;
    bool Activated;

    public void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    public override void Activate()
    {
        if (Activated)
            return;
        Activated  = true;
        animator.Play("Open");
        audioSource.PlayOneShot(OpenSounds.GetRandom());
    }
    public override void Deactivate()
    {
        if (!Activated)
            return;
        Activated  = false;
        animator.Play("Close");
        audioSource.PlayOneShot(CloseSounds.GetRandom());
    }
}
