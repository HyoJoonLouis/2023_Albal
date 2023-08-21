using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : EnergyObject
{
    [SerializeField] RandomSounds<AudioClip> OpenSounds;
    [SerializeField] RandomSounds<AudioClip> CloseSounds;

    Animator animator;
    AudioSource audioSource;

    public void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    public override void Activate()
    {
        animator.Play("Open");
        audioSource.PlayOneShot(OpenSounds.GetRandom());
    }
    public override void Deactivate()
    {
        animator.Play("Close");
        audioSource.PlayOneShot(CloseSounds.GetRandom());
    }
}
