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
    Transform CurrentCheckPoint;

    public void TakeDamage(float value)
    {
        currentHp -= value;
        if (currentHp < 0)
        {
            
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
    }
}
