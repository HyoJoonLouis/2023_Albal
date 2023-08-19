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
    [SerializeField] Transform CurrentCheckPoint;

    [Header("UI")]
    [SerializeField] Animator GameOverAnimator;

    public void TakeDamage(float value)
    {
        currentHp -= value;
        if (currentHp <= 0)
        {
            GameOverAnimator.Play("GameOverAnimation");
            Time.timeScale = 0.0f;
            Invoke("SpawnCharacter", 1.0f);
        }
    }

    void Start()
    {
        currentHp = MaxHp;
    }

    public void SetCheckPoint(Transform checkpoint)
    {
        CurrentCheckPoint = checkpoint;
    }

    public void SpawnCharacter()
    {
        Time.timeScale = 1.0f;
        this.transform.position = CurrentCheckPoint.position;
    }
}
