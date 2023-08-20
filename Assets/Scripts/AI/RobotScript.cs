using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RobotScript : MonoBehaviour, IDamagable
{
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

    RobotStates state;
    NavMeshAgent agent;
    TargetDetectScript targetDetectScript;
    Animator animator;
    OnSightDetectScript onSightDetectScript;
    AudioSource audioSource;

    private void Awake()
    {
        currentHp = MaxHp;
        state = new RobotStates();
        agent = GetComponent<NavMeshAgent>();
        targetDetectScript = GetComponentInChildren<TargetDetectScript>();
        animator = GetComponent<Animator>();
        onSightDetectScript = GetComponentInChildren<OnSightDetectScript>();
        audioSource = GetComponent<AudioSource>();
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
    }

    public void OnAttackSoundPlay()
    {
        audioSource.PlayOneShot(OnAttackSounds.GetRandom());
        ObjectPoolManager.SpawnObject(RobotEyeShineParticle, RobotEyeShineParticlePosition.position, RobotEyeShineParticlePosition.rotation);
    }


    public void TakeDamage(float value)
    {
        currentHp -= value;
        changeRenderScript.ChangeRender();
        audioSource.PlayOneShot(OnHitSounds.GetRandom());
        if(currentHp <= 0)
        {
            state.ChangeState(RobotState.die, animator, agent, Vector3.zero);
            ObjectPoolManager.SpawnObject(RobotDestroyParticle, RobotDestroyParticlePosition.position, RobotDestroyParticlePosition.rotation);
            audioSource.PlayOneShot(OnDieSounds.GetRandom());
        }
    }
}
