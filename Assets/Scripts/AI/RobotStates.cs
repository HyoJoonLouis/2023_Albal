using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public enum RobotState
{
    watching,
    run,
    attack
}

public class RobotStates
{
    public RobotState state;

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
            animator.Play("Watching");

        else if (state == RobotState.run)
        {
            animator.SetBool("AttackToRun", true);
            agent.enabled = true;
            agent.SetDestination(target);
        }

        else if (state == RobotState.attack)
        {
            animator.SetBool("AttackToRun", false);
            agent.enabled = false;
            animator.Play("Attack");
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
