using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    [SerializeField] protected BaseEnemyCharacter Owner;

    [SerializeField] protected GameObject TargetObject;
    [SerializeField] protected AIRotate AIRotate;

    protected AIMovement AIMovement;
    protected bool IsDetectTarget;

    protected Vector3 StartPosition;
    [HideInInspector] public AttackType CurrentAttackType;

    public bool SetDetectTarget
    {
        set { SetDetectTarget = value; }
    }

    protected virtual void Start()
    {
        AIMovement = GetComponent<AIMovement>();
    }

    public virtual void EnableSkillDetectTrigger(AttackType attackType)
    {
        if (Owner.UseSkill(attackType))
        {
            if(AIMovement)
            {
                AIMovement.StopMove();
            }
        }
    }

    public virtual void SetCurrentAttackType(AttackType attackType)
    {
        CurrentAttackType = attackType;
    }

    public virtual void EndOfSkill(AttackType attackType)
    {
    }
}
