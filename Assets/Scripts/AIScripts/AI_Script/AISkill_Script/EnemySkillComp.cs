using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class EnemySkillComp : MonoBehaviour
{
    [HideInInspector] public AttackType attackType;

    private BaseEnemyController Controller;
    private float SkillAttackDamage;

    private GameObject SkillUseRadius;

    private float MaxCoolTime;
    private float SkillAnimationTime;

    [HideInInspector] public bool IsCoolDown;

    public void SetSkillInfo(AttackType type, float _SkillAttackDamage, float _SkillAnimationTime, GameObject _SkillUseRadius, float _MaxCoolTime, bool _HaveCoolTimeWhenStart, float _DamageTime)
    {
        attackType = type;
        SkillAttackDamage = _SkillAttackDamage;
        SkillUseRadius = _SkillUseRadius;
        MaxCoolTime = _MaxCoolTime;
        IsCoolDown =  _HaveCoolTimeWhenStart;
        SkillAnimationTime = _SkillAnimationTime;
        Controller = GetComponent<BaseEnemyController>();
        if (IsCoolDown)
        {
            StartCoolDown();
        }
    }

    public void EnableSkill()
    {
        SkillUseRadius.SetActive(true);
        StartCoroutine(StartCoolDown());
        StartCoroutine(StartCoolDown());
        StartCoroutine(StartAnimationTimer());
    }

    IEnumerator StartCoolDown()
    {
        IsCoolDown = true;
        yield return new WaitForSeconds(MaxCoolTime);
        IsCoolDown = false;
        if(attackType == Controller.CurrentAttackType)
        Controller.EnableSkillDetectTrigger(attackType);
    }

    IEnumerator StartDamageTimer()
    {
        yield return new WaitForSeconds(DamageTime);
        if()
    }

    IEnumerator StartAnimationTimer()
    {
        yield return new WaitForSeconds(SkillAnimationTime);
        Controller.EndOfSkill(attackType);
    }
}
