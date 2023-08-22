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
    Vector3 InitializePosition;
    Quaternion InitializeRotation;


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

    [SerializeField] GameObject AfterSound;


    public void TakeDamage(float value)
    {
        currentHp -= value;
        audioSource.PlayOneShot(OnHitSounds.GetRandom());
        changeRenderScript.ChangeRender();
        if(currentHp <= 0)
        {
            GameManager.Instance.Player.DieDelegate -= SetInitialPosition;
            GameObject.Destroy(this.gameObject);
            ObjectPoolManager.SpawnObject(AfterSound, transform.position, transform.rotation).GetComponent<BulletSound>().PlaySound(OnDieSounds.GetRandom());
            ObjectPoolManager.SpawnObject(RobotDestroyParticle, RobotDestroyParticlePosition.position, RobotDestroyParticlePosition.rotation);
        }
    }

    void Start()
    {
        targetDetectScript = GetComponentInChildren<TargetDetectScript>();
        onSightDetectScript = GetComponentInChildren<OnSightDetectScript>();
        animator = GetComponent<Animator>();
        currentHp = MaxHp;
        audioSource = GetComponent<AudioSource>();
        InitializePosition = this.transform.position;
        InitializeRotation = this.transform.rotation;
        GameManager.Instance.Player.DieDelegate += SetInitialPosition;
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
            TurretBulletScript bullet = ObjectPoolManager.SpawnObject(AttackBullet, BulletSpawnLocation.position, BulletSpawnLocation.rotation).GetComponent<TurretBulletScript>();
            bullet.rb.velocity = (targetDetectScript.Target.transform.position - BulletSpawnLocation.position).normalized * speed;
            audioSource.PlayOneShot(OnAttackSounds.GetRandom());
        }
        yield return new WaitForSeconds(AttackCoolTime);
        StartCoroutine("Attack");
    }

    public void SetInitialPosition()
    {
        this.transform.position = InitializePosition;
        this.transform.rotation = InitializeRotation;
    }
}
