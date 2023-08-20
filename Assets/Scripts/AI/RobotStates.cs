using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

        state = newState;

        if (state == RobotState.watching)
            Watching(animator);

        else if (state == RobotState.run)
            Run(animator, agent, target);

        else if (state == RobotState.attack)
            Attack(animator, agent);
    }

    private void Watching(Animator animator)
    {
        animator.Play("Watching");
    }

    private void Run(Animator animator, NavMeshAgent agent, Vector3 target)
    {
        animator.SetBool("AttackToRun", true);
        agent.enabled = true;
        agent.SetDestination(target);
    }

    private void Attack(Animator animator, NavMeshAgent agent)
    {
        animator.SetBool("AttackToRun", false);
        agent.enabled = false;
        animator.Play("Attack");
    }

    public void UpdateState(NavMeshAgent agent, Vector3 target)
    {
        if(state == RobotState.run)
        {
            agent.SetDestination(target);
        }
    }
}
