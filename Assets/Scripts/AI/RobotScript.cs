using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RobotScript : MonoBehaviour, IDamagable
{
    [Header("Properties")]
    [SerializeField] float MaxHp;
    [SerializeField] float currentHp;

    [SerializeField] float Distance;
 
    [Header("Initialize")]
    [SerializeField] Collider RightHandCollider;

    RobotStates state;
    NavMeshAgent agent;
    TargetDetectScript targetDetectScript;
    Animator animator;
    OnSightDetectScript onSightDetectScript;

    private void Awake()
    {
        currentHp = MaxHp;
        state = new RobotStates();
        agent = GetComponent<NavMeshAgent>();
        targetDetectScript = GetComponentInChildren<TargetDetectScript>();
        animator = GetComponent<Animator>();
        onSightDetectScript = GetComponentInChildren<OnSightDetectScript>();
    }

    public void Update()
    {
        if(targetDetectScript.Target != null && Vector3.Distance(targetDetectScript.Target.transform.position, transform.position) <= Distance)
        {
            state.ChangeState(RobotState.attack, animator, agent, targetDetectScript.Target.transform.position);
        }
        else if(targetDetectScript.Target && onSightDetectScript.DetectTarget(targetDetectScript.Target.transform.position))
        {
            state.ChangeState(RobotState.run, animator, agent, targetDetectScript.Target.transform.position);
        }
        else
        {
            state.ChangeState(RobotState.watching, animator, agent, Vector3.zero);
        }

        state.UpdateState(agent, targetDetectScript.Target.transform.position);
    }

    public void SetTriggerOn()
    {
        RightHandCollider.enabled = true;
    }

    public void SetTriggerOff()
    {
        RightHandCollider.enabled = false;
    }

    public void TakeDamage(float value)
    {
        currentHp -= value;
        if(currentHp <= 0)
        {

        }
    }
}
