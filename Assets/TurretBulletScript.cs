using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletScript : MonoBehaviour
{
    [SerializeField] float Damage;
    public Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        IDamagable damagable = collision.transform.GetComponent<IDamagable>();
        if (damagable != null)
            damagable.TakeDamage(Damage);
    }
}
