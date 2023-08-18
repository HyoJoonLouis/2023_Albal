using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private float ProjectileDamage;

    public void InitProjectile(Vector3 StartPos, Quaternion Rotation)
    {
        transform.position = StartPos;
        transform.rotation = Rotation;
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * ProjectileSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        other.GetComponent<IDamagable>().TakeDamage(ProjectileDamage);
    }

    IEnumerator StartLifeTime()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
