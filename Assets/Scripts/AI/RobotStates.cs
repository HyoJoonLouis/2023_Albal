using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public enum RobotState
{
    watching,
    run,
    attack,
    die
}

public class RobotStates
{
    public RobotState state;

    public bool IsDetected;

    public void ChangeState(RobotState newState, Animator animator, NavMeshAgent agent, Vector3 target)
    {
        if (state == newState)
            return;

        OnEnd();
        state = newState;
        OnStart(newState, animator, agent, target);
    }

    private void OnStart(RobotState newState, Animator animator, NavMeshAgent agent, Vector3 target)
    {
        if (state == RobotState.watching)
        {
            animator.SetBool("AttackToRun", false);
            animator.Play("Watching");
            agent.enabled = false;
            IsDetected = false;
        }
        else if (state == RobotState.run)
        {
            animator.SetBool("AttackToRun", true);
            agent.enabled = true;
            IsDetected = true;
        }

        else if (state == RobotState.attack)
        {
            animator.SetBool("AttackToRun", false);
            agent.enabled = false;
            animator.Play("Attack");
        }

        else if (state == RobotState.die)
        {
            animator.Play("Dead");
        }
    }

    public void OnUpdate(NavMeshAgent agent, Vector3 target)
    {
        if (state == RobotState.run)
        {
            agent.SetDestination(target);
        }
    }

    private void OnEnd()
    {

    }
}
