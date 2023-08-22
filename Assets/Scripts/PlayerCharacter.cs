using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamagable
{
    [Header("Stat")]
    [SerializeField] float MaxHp;
    [SerializeField] float currentHp;

    [Header("Check Point")]
    [SerializeField] Vector3 CurrentCheckPoint;

    [Header("UI")]
    [SerializeField] Animator GameOverUI;
    [SerializeField] Animator OnHitUI;

    [Header("Sounds")]
    [SerializeField] AudioSource BackgroundSound;
    [SerializeField] RandomSounds<AudioClip> OnHitSounds;
    AudioSource audioSource;


    public delegate void OnDieDelegate();
    public OnDieDelegate DieDelegate;
    public void TakeDamage(float value)
    {
        currentHp -= value;
        audioSource.PlayOneShot(OnHitSounds.GetRandom());
        OnHitUI.Play("Hit");
        if (currentHp < 0)
        {
            DieDelegate();
        }
    }

    void Awake()
    {
        currentHp = MaxHp;
        audioSource = GetComponent<AudioSource>();
        DieDelegate += OnPlayerDie;
    }

    void Update()
    {
        if(this.transform.position.y <= -10)
        {
            DieDelegate();
        }
    }

    void OnPlayerDie()
    {
        GameOverUI.Play("Open");
        Invoke("SpawnCharacter", 3.0f);
    }

    public void SetCheckPoint(Vector3 checkpoint)
    {
        CurrentCheckPoint = checkpoint;
    }

    public void SpawnCharacter()
    {
        this.transform.position = CurrentCheckPoint;
        currentHp = MaxHp;
        GameOverUI.Play("Idle");
        BackgroundSound.volume = 0.28f;
    }
}
