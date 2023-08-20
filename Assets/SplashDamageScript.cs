using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageScript : MonoBehaviour
{
    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        IDamagable damagable = other.GetComponent<IDamagable>();
        if(damagable != null)
        {
            damagable.TakeDamage(Damage);
        }
    }
}
