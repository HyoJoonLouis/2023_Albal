using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkScript : MonoBehaviour, IDamagable
{
    [SerializeField] float MaxHp;
    [SerializeField] float currentHp;

    [SerializeField] LandMarkDoorSript Door;

    [SerializeField] bool isLastLandmark;

    [Header("Sounds")]
    [SerializeField] RandomSounds<AudioClip> ExplodeSounds;
    [SerializeField] RandomSounds<AudioClip> GameClearSounds;
    AudioSource audioSource;

    Animator animator;
    ChangeRenderScript changeRenderScript;
    bool isDead = false;
    public void TakeDamage(float value)
    {
        if (isDead)
            return;
        currentHp -= value;
        changeRenderScript.ChangeRender();
        if( currentHp < 0 )
        {
            isDead = true;
            animator.Play("Explode");
            if(isLastLandmark)
            {
                GameObject.Find("GameClear").GetComponent<Animator>().Play("Open");
                return;
            }
        }
    }

    public void OpenDoor()
    {
        if (!Door)
            return;

        Door.Open();
    }

    public void ExplodeStart()
    {
        audioSource.PlayOneShot(ExplodeSounds.GetRandom());
    }
   

    void Start()
    {
        currentHp = MaxHp;
        changeRenderScript = GetComponent<ChangeRenderScript>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnExplodeEnd()
    {
        Destroy(this.gameObject);
    }
}
