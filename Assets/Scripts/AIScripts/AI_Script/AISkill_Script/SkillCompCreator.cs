using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SkillInfo
{
    public AttackType attackType;
    public float SkillAttackDamage;

    [Header("Skill Cool Time")]
    public float MaxCoolTime;
    [Header("Have Cool Time When Start")]
    public bool HaveCoolTimeWhenStart;
    [Header("Skill Animation Time")]
    public float SkillAnimationTime;
    [Header("Damage Affect Time")]
    public float DamageTime;

    [Header("Skill Radius Trigger (Need Trigger Prefabs)")]
    public GameObject SkillUseRadius;
}

[System.Serializable]
public struct SkillInfoStruct
{
    [Header("Character Skill Info List")]
    public List<SkillInfo> SkillInfoList;
}

public class SkillCompCreator : MonoBehaviour
{
    [SerializeField] protected SkillInfoStruct SkillInfoStr;

    protected virtual void Awake() 
    {
        for (int i = 0; i < SkillInfoStr.SkillInfoList.Count; i++)
        {
            EnemySkillComp CreateComp = gameObject.AddComponent<EnemySkillComp>();
            CreateComp.SetSkillInfo(SkillInfoStr.SkillInfoList[i].attackType,
                SkillInfoStr.SkillInfoList[i].SkillAttackDamage, SkillInfoStr.SkillInfoList[i].SkillAnimationTime, SkillInfoStr.SkillInfoList[i].SkillUseRadius,);
        }

        SkillInfoStr.SkillInfoList.Clear();
        Destroy(this);
    }
}
