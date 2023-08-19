using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRightHandScript : MonoBehaviour
{
    [SerializeField] float Damage;

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IDamagable>().TakeDamage(Damage);
    }
}
