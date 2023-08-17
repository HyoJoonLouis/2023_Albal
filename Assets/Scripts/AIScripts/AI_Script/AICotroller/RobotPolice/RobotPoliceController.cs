using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPoliceController : BaseEnemyController
{    
    [SerializeField] private DetectRadius DetectRadius;

    protected override void Start()
    {
        base.Start();

        AIMovement.SetTargetTransform = TargetObject.transform;

        StartPosition = transform.position;

        StartCoroutine(DetectTarget());
    }

    public override void EnableSkillDetectTrigger(AttackType attackType)
    {
        base.EnableSkillDetectTrigger(attackType);
    }

    public override void SetCurrentAttackType(AttackType attackType)
    {
        base.SetCurrentAttackType(attackType);
    }

    public override void EndOfSkill(AttackType attackType)
    {
        AIMovement.StartMove();
    }

    private IEnumerator DetectTarget()
    {
        while(!IsDetectTarget)
        {
            IsDetectTarget = DetectRadius.DetectTarget(TargetObject.transform.position);

            if(IsDetectTarget)
            {
                AIMovement.StartMove();
            }

            yield return null;
        }
    }
}
