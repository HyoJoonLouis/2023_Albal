using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : BaseEnemyCharacter
{
    [SerializeField] private EnemyProjectile EnemyProjectile;
    [SerializeField] private Transform ProjectileOriginTransform;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool UseSkill(AttackType attackType)
    {
        if(base.UseSkill(attackType))
        {
            Instantiate(EnemyProjectile).InitProjectile(ProjectileOriginTransform.position, ProjectileOriginTransform.rotation);
            return true;
        }
        return false;
    }
}