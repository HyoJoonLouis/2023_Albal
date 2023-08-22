using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageScript : MonoBehaviour
{
    public float Damage;

    private void OnEnable()
    {
        Invoke("ReturnObjectToPool", 0.4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if(damagable != null)
        {
            damagable.TakeDamage(Damage);
        }
    }

    private void ReturnObjectToPool()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
