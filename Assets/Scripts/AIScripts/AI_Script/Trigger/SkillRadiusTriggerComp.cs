using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRadiusTriggerComp : BaseTriggerComp
{
    [SerializeField] private AttackType AttackType;
    private BaseEnemyController Controller;
    protected override void Awake()
    {
        base.Awake();
        Controller = transform.parent.GetComponent<BaseEnemyController>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Controller.SetCurrentAttackType(AttackType);
        Controller.EnableSkillDetectTrigger(AttackType);
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if(AttackType == Controller.CurrentAttackType)
        Controller.SetCurrentAttackType(AttackType.NONE);
    }
}