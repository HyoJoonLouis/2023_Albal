using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    NONE,
    ATTACK0,
    ATTACK1,
    ATTACK2,
    ATTACK3,
}


public class BaseEnemyCharacter : BaseCharacter
{
    [Header("Character HP")]
    [SerializeField] protected float EnemyMaxHP;
    [SerializeField] protected float EnemyCurrentHP;

    [Header("Character MovementSpeed")]
    [SerializeField] protected float OriginMovementSpeed;
    [SerializeField] protected float ChaseSpeed;

    public delegate void AIStateFunc();

    public Dictionary<AttackType, EnemySkillComp> SkillCompDict =
        new Dictionary<AttackType, EnemySkillComp>();

    protected override void Awake() 
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        EnemySkillComp[] EnemySkillList = GetComponents<EnemySkillComp>();

        for (int i = 0; i < EnemySkillList.Length; i++)
        {
            SkillCompDict.Add(AttackType.ATTACK0 + i, EnemySkillList[i]);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
    }

    public virtual bool UseSkill(AttackType attackType)
    {
        if (!SkillCompDict[attackType].IsCoolDown)
        {
            SkillCompDict[attackType].EnableSkill();
            return true;
        }
        return false;
    }
}
