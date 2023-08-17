using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERobotAnimType
{ 
    IDLE,
    ALERT,
    RUN,
    ATTACK,
    DEAD,
}

public class RobotPoliceController : BaseEnemyController
{    
    [SerializeField] private DetectRadius DetectRadius;
    [SerializeField] private Animator animator;
    [HideInInspector]public ERobotAnimType CurrentAnimType;

    protected override void Start()
    {
        base.Start();

        AIMovement.SetTargetTransform = TargetObject.transform;

        StartPosition = transform.position;

        StartCoroutine(DetectTarget());

        SetAnimationType(ERobotAnimType.IDLE);
    }

    public void SetAnimationType(ERobotAnimType type)
    {
        CurrentAnimType = type;
        animator.SetInteger("AnimationType", (int)CurrentAnimType);
    }

    public override void EnableSkillDetectTrigger(AttackType attackType)
    {
        base.EnableSkillDetectTrigger(attackType);
        SetAnimationType(ERobotAnimType.ATTACK);
    }

    public override void SetCurrentAttackType(AttackType attackType)
    {
        base.SetCurrentAttackType(attackType);
    }

    public override void EndOfSkill(AttackType attackType)
    {
        AIMovement.StartMove();
        SetAnimationType(ERobotAnimType.RUN);
    }

    private IEnumerator DetectTarget()
    {
        while(!IsDetectTarget)
        {
            IsDetectTarget = DetectRadius.DetectTarget(TargetObject.transform.position);

            if (IsDetectTarget)
            {
                AIMovement.StartMove();
                SetAnimationType(ERobotAnimType.RUN);
            }
            else
            {
                SetAnimationType(ERobotAnimType.ALERT);
            }

            yield return null;
        }
    }
}
