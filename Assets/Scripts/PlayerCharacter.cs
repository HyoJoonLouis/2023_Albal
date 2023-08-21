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
    [SerializeField] Animator GameOverUI;

    public void TakeDamage(float value)
    {
        currentHp -= value;
        if (currentHp < 0)
        {
            GameOverUI.Play("GameOver");
            Invoke("SpawnCharacter", 3.0f);

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
        this.transform.position = CurrentCheckPoint.position;
        currentHp = MaxHp;
        GameOverUI.Play("Idle");
    }
}
