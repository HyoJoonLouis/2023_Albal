using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkScript : MonoBehaviour, IDamagable
{
    [SerializeField] float MaxHp;
    [SerializeField] float currentHp;

    [SerializeField] LandMarkDoorSript Door;
    [SerializeField] bool isLastLandMark;

    [Header("Sounds")]
    [SerializeField] RandomSounds<AudioClip> OnHitSounds;
    [SerializeField] RandomSounds<AudioClip> OnDieSounds;
    AudioSource audioSource;


    bool isDead = false;
    public void TakeDamage(float value)
    {
        if (isDead)
            return;
        currentHp -= value;
        audioSource.PlayOneShot(OnHitSounds.GetRandom());
        if( currentHp < 0 )
        {
            if(isLastLandMark)
            {
                Animator animator = GameObject.Find("GameClear").GetComponent<Animator>();
                animator.Play("GameClear");
            }

            Door.Open();
            audioSource.PlayOneShot(OnDieSounds.GetRandom());
            isDead = true;
        }
    }

    void Start()
    {
        currentHp = MaxHp;
        audioSource = GetComponent<AudioSource>();
    }
}
