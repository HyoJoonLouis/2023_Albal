using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour, IDamagable
{
    [Header("Initialize")]
    [SerializeField] float MaxHp;
    [SerializeField] float currentHp;
    [SerializeField] GameObject RobotDestroyParticle;
    [SerializeField] Transform RobotDestroyParticlePosition;
    [SerializeField] ChangeRenderScript changeRenderScript;


    [Header("Bullet")]
    [SerializeField] Transform BulletSpawnLocation;
    [SerializeField] GameObject AttackBullet;
    [SerializeField] float AttackCoolTime;
    [SerializeField] float speed;

    [Header("Sounds")]
    [SerializeField] RandomSounds<AudioClip> OnAttackSounds;
    [SerializeField] RandomSounds<AudioClip> OnHitSounds;
    [SerializeField] RandomSounds<AudioClip> OnDieSounds;

    AudioSource audioSource;
    TargetDetectScript targetDetectScript;
    OnSightDetectScript onSightDetectScript;
    Animator animator;


    public void TakeDamage(float value)
    {
        currentHp -= value;
        changeRenderScript.ChangeRender();
        audioSource.PlayOneShot(OnHitSounds.GetRandom());
        if(currentHp <= 0)
        {
            GameObject.Destroy(this.gameObject);
            audioSource.PlayOneShot(OnDieSounds.GetRandom());
            ObjectPoolManager.SpawnObject(RobotDestroyParticle, RobotDestroyParticlePosition.position, RobotDestroyParticlePosition.rotation);
        }
    }

    void Awake()
    {
        targetDetectScript = GetComponentInChildren<TargetDetectScript>();
        onSightDetectScript = GetComponentInChildren<OnSightDetectScript>();
        animator = GetComponent<Animator>();
        currentHp = MaxHp;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("Attack");
    }

    void Update()
    {
        if (targetDetectScript.Target && onSightDetectScript.DetectTarget(targetDetectScript.Target.transform.position))
            transform.LookAt(targetDetectScript.Target.transform);
    }

    IEnumerator Attack()
    {
        if (targetDetectScript.Target != null && onSightDetectScript.DetectTarget(targetDetectScript.Target.transform.position))
        {
            TurretBulletScript bullet = ObjectPoolManager.SpawnObject(AttackBullet, transform.position, transform.rotation).GetComponent<TurretBulletScript>();
            bullet.rb.velocity = (targetDetectScript.Target.transform.position - transform.position).normalized * speed;
            audioSource.PlayOneShot(OnAttackSounds.GetRandom());
        }
        yield return new WaitForSeconds(AttackCoolTime);
        StartCoroutine("Attack");
    }

}
