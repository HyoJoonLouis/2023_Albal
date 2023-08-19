using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class SkillUseTrigger : BaseTriggerComp
{
    [SerializeField] private AttackType AttackType;
    [SerializeField] private float Damage;
    [SerializeField] private float AffectDamageTime;

    private GameObject TargetObject;


    IEnumerator StartDamageTimer()
    {
        yield return new WaitForSeconds(AffectDamageTime);
        if (TargetObject != null)
            TargetObject.GetComponent<IDamagable>().TakeDamage(Damage);
        StartCoroutine(StartDamageTimer());
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        TargetObject = other.gameObject;
        StartCoroutine(StartDamageTimer());
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        TargetObject = null;
        StopCoroutine(StartDamageTimer());
    }
}
