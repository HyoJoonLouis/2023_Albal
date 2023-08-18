using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPolice : BaseEnemyCharacter
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override bool UseSkill(AttackType attackType)
    {
        return base.UseSkill(attackType);
    }

    //public override void TakeDamage(float Damage)
    //{
    //    base.TakeDamage(Damage);
    //    if(EnemyCurrentHP <= 0)
    //    {
    //        SetAnimationType(ERobotAnimType.DEAD);
    //    }
    //}

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
    }
}