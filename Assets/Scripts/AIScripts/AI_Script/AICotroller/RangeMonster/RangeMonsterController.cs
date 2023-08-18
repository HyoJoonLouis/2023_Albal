using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeMonsterController : BaseEnemyController
{
    protected override void Start()
    {
        base.Start();
        AIRotate.SetTargetTransform = TargetObject.transform;
    }

    public override void EnableSkillDetectTrigger(AttackType attackType)
    {
        AIRotate.StartRotate();
        base.EnableSkillDetectTrigger(attackType);
    }

    public override void SetCurrentAttackType(AttackType attackType)
    {
        base.SetCurrentAttackType(attackType);
    }

    public override void EndOfSkill(AttackType attackType)
    {

    }
}