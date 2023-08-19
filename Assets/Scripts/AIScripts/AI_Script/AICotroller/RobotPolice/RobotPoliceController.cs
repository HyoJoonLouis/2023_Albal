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


    protected override void Start()
    {
        base.Start();

        AIMovement.SetTargetTransform = TargetObject.transform;

        StartPosition = transform.position;

        StartCoroutine(DetectTarget());

        Owner.SetAnimationType(ERobotAnimType.IDLE);
    }

    public override void EnableSkillDetectTrigger(AttackType attackType)
    {
        base.EnableSkillDetectTrigger(attackType);
        Owner.SetAnimationType(ERobotAnimType.ATTACK);
    }

    public override void SetCurrentAttackType(AttackType attackType)
    {
        base.SetCurrentAttackType(attackType);
    }

    public override void EndOfSkill(AttackType attackType)
    {
        AIMovement.StartMove();
        Owner.SetAnimationType(ERobotAnimType.RUN);
    }

    private IEnumerator DetectTarget()
    {
        while(!IsDetectTarget)
        {
            IsDetectTarget = DetectRadius.DetectTarget(TargetObject.transform.position);

            if (IsDetectTarget)
            {
                AIMovement.StartMove();
                Owner.SetAnimationType(ERobotAnimType.RUN);
            }
            else
            {
                Owner.SetAnimationType(ERobotAnimType.ALERT);
            }

            yield return null;
        }
    }
}
