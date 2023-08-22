using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RobotScript : MonoBehaviour, IDamagable
{
    Vector3 initialPosition;
    Quaternion initialRotation;

    [Header("Properties")]
    [SerializeField] float MaxHp;
    [SerializeField] float currentHp;

    [Header("Sounds")]
    [SerializeField] RandomSounds<AudioClip> ReadyAttackSounds;
    [SerializeField] RandomSounds<AudioClip> OnAttackSounds;
    [SerializeField] RandomSounds<AudioClip> OnHitSounds;
    [SerializeField] RandomSounds<AudioClip> OnDieSounds;

    [Header("Initialize")]
    [SerializeField] GameObject RobotEyeShineParticle;
    [SerializeField] Transform RobotEyeShineParticlePosition;
    [SerializeField] GameObject RobotDestroyParticle;
    [SerializeField] Transform RobotDestroyParticlePosition;
    [SerializeField] float Distance;
    [SerializeField] Collider RightHandCollider;
    [SerializeField] ChangeRenderScript changeRenderScript;
    [SerializeField] GameObject AfterSound;

    RobotStates state;
    NavMeshAgent agent;
    TargetDetectScript targetDetectScript;
    Animator animator;
    OnSightDetectScript onSightDetectScript;
    AudioSource audioSource;

    private void Start()
    {
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;
        GameManager.Instance.Player.DieDelegate += SetToInitialPosition;
        currentHp = MaxHp;
        state = new RobotStates();
        agent = GetComponent<NavMeshAgent>();
        targetDetectScript = GetComponentInChildren<TargetDetectScript>();
        animator = GetComponent<Animator>();
        onSightDetectScript = GetComponentInChildren<OnSightDetectScript>();
        audioSource = GetComponent<AudioSource>();

        state.ChangeState(RobotState.watching, animator, agent, Vector3.zero);
    }

    public void Update()
    {
        if(targetDetectScript.Target != null && Vector3.Distance(targetDetectScript.Target.transform.position, transform.position) <= Distance)
        {
            state.ChangeState(RobotState.attack, animator, agent, targetDetectScript.Target.transform.position);
        }
        else if(targetDetectScript.Target != null && onSightDetectScript.DetectTarget(targetDetectScript.Target.transform.position))
        {
            SetTriggerOff();
            state.ChangeState(RobotState.run, animator, agent, targetDetectScript.Target.transform.position);
        }
        else if(targetDetectScript.Target != null && state.IsDetected)
        {
            SetTriggerOff();
            state.ChangeState(RobotState.run, animator, agent, targetDetectScript.Target.transform.position);
        }
        else
        {
            SetTriggerOff();
            state.ChangeState(RobotState.watching, animator, agent, Vector3.zero);
        }

        state.OnUpdate(agent, targetDetectScript.Target ? targetDetectScript.Target.transform.position : Vector3.zero) ;
    }

    public void SetTriggerOn()
    {
        RightHandCollider.enabled = true;
    }

    public void SetTriggerOff()
    {
        RightHandCollider.enabled = false;
    }

    public void ReadyAttackSoundPlay()
    {
        audioSource.PlayOneShot(ReadyAttackSounds.GetRandom());
        ObjectPoolManager.SpawnObject(RobotEyeShineParticle, RobotEyeShineParticlePosition.position, RobotEyeShineParticlePosition.rotation).GetComponent<Transform>().parent = RobotEyeShineParticlePosition;
    }

    public void OnAttackSoundPlay()
    {
        audioSource.PlayOneShot(OnAttackSounds.GetRandom());
    }

    public void OnDied()
    {
    }

    public void SetToInitialPosition()
    {
        this.transform.position = initialPosition;
        this.transform.rotation = initialRotation;
        state.ChangeState(RobotState.watching, animator, agent, Vector3.zero);
    }
    public void TakeDamage(float value)
    {
        currentHp -= value;
        changeRenderScript.ChangeRender();
        audioSource.PlayOneShot(OnHitSounds.GetRandom());
        if(currentHp <= 0)
        {
            GameManager.Instance.Player.DieDelegate -= SetToInitialPosition;
            state.ChangeState(RobotState.die, animator, agent, Vector3.zero);
            ObjectPoolManager.SpawnObject(RobotDestroyParticle, RobotDestroyParticlePosition.position, RobotDestroyParticlePosition.rotation);
            Destroy(this.gameObject);
            ObjectPoolManager.SpawnObject(AfterSound, transform.position, transform.rotation).GetComponent<BulletSound>().PlaySound(OnDieSounds.GetRandom());
        }
    }
}
