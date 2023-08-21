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
        if (!other.transform.CompareTag("Player") || isDead == true)
            return;
        Open();

    }


    public void Open()
    {
        animator.Play("Open");
        audioSource.PlayOneShot(OpenSounds.GetRandom());
        isDead = true;
        ObjectPoolManager.SpawnObject(OpenParticle, OpenParticlePosition.position, OpenParticlePosition.rotation);
        ObjectPoolManager.SpawnObject(WhileOpenParticle, WhileOpenParticlePosition.position, WhileOpenParticlePosition.rotation);

    }
}
