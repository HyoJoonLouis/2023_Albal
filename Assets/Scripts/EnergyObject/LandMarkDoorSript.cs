using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkDoorSript : MonoBehaviour
{
    [SerializeField] RandomSounds<AudioClip> OpenSounds;
    [SerializeField] RandomSounds<AudioClip> CloseSounds;

    [SerializeField] GameObject OpenParticle;
    [SerializeField] GameObject WhileOpenParticle;

    [SerializeField] Transform OpenParticlePosition;
    [SerializeField] Transform WhileOpenParticlePosition;

    AudioSource audioSource;
    Animator animator;
    bool isDead = false;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isDead || !other.transform.CompareTag("Player"))
            return;
        
        Open();
        isDead = true;
        this.enabled = false;
    }


    public void Open()
    {
        if (isDead)
            return;

        animator.Play("Open");
        audioSource.PlayOneShot(OpenSounds.GetRandom());
        ObjectPoolManager.SpawnObject(OpenParticle, OpenParticlePosition.position, OpenParticlePosition.rotation);
        ObjectPoolManager.SpawnObject(WhileOpenParticle, WhileOpenParticlePosition.position, WhileOpenParticlePosition.rotation);
        isDead = true;
    }
}
