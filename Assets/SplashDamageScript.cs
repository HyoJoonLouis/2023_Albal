using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageScript : MonoBehaviour
{
    List<IDamagable> damagables = new List<IDamagable>();
    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        damagables.Add(other.GetComponent<IDamagable>());
        if (damagables.Count > 0)
        {
            foreach(var damagable in damagables)
            {
                damagable.TakeDamage(Damage);
            }
        }

        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
