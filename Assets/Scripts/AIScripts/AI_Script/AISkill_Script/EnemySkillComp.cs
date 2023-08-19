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

    public void SetSkillInfo(AttackType type, float _SkillAttackDamage, float _SkillAnimationTime, GameObject _SkillUseRadius)
    {
        attackType = type;
        SkillAttackDamage = _SkillAttackDamage;
        SkillUseRadius = _SkillUseRadius;
        SkillAnimationTime = _SkillAnimationTime;
        Controller = GetComponent<BaseEnemyController>();
    }

    public void EnableSkill()
    {
        SkillUseRadius.SetActive(true);
    }


}
