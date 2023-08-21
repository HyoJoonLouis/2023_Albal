using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkScript : MonoBehaviour, IDamagable
{
    [SerializeField] float MaxHp;
    [SerializeField] float currentHp;

    [SerializeField] LandMarkDoorSript Door;
    bool isDead = false;
    public void TakeDamage(float value)
    {
        if (isDead)
            return;
        currentHp -= value;
        if( currentHp < 0 )
        {
            Door.Open();
            isDead = true;
        }
    }

    void Start()
    {
        currentHp = MaxHp;
    }
}
