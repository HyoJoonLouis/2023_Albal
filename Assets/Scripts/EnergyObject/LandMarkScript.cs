using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkScript : MonoBehaviour, IDamagable
{
    [SerializeField ]float MaxHp;
    float currentHp;

    [SerializeField] LandMarkDoorSript Door;
    public void TakeDamage(float value)
    {
        currentHp -= value;
        if( currentHp < 0 )
        {
            Door.Open();
        }
    }

    void Start()
    {
        currentHp = MaxHp;
    }
}
