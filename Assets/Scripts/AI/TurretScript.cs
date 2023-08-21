using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour, IDamagable
{
    [Header("Initialize")]
    [SerializeField] float MaxHp;
    [SerializeField] float currentHp;

    [Header("Bullet")]
    [SerializeField] Transform BulletSpawnLocation;
    [SerializeField] GameObject AttackBullet;
    [SerializeField] float AttackCoolTime;
    [SerializeField] float speed;


    TargetDetectScript targetDetectScript;
    OnSightDetectScript onSightDetectScript;
    Animator animator;


    public void TakeDamage(float value)
    {
        currentHp -= value;
        if(currentHp <= 0)
        {

        }
    }

    void Awake()
    {
        targetDetectScript = GetComponentInChildren<TargetDetectScript>();
        onSightDetectScript = GetComponentInChildren<OnSightDetectScript>();
        animator = GetComponent<Animator>();
        currentHp = MaxHp;
        StartCoroutine("Attack");
    }

    void Update()
    {
        if (targetDetectScript.Target && onSightDetectScript.DetectTarget(targetDetectScript.Target.transform.position))
            transform.LookAt(targetDetectScript.Target.transform);

    }

    IEnumerator Attack()
    {
        if (targetDetectScript.Target && onSightDetectScript.DetectTarget(targetDetectScript.Target.transform.position))
        {
            TurretBulletScript bullet = ObjectPoolManager.SpawnObject(AttackBullet, transform.position, transform.rotation).GetComponent<TurretBulletScript>();
            bullet.rb.velocity = (targetDetectScript.Target.transform.position - transform.position).normalized * speed;
        }
        yield return new WaitForSeconds(AttackCoolTime);
        StartCoroutine("Attack");
    }

}
