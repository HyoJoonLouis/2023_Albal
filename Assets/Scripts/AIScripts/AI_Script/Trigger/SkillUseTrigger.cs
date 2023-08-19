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

<<<<<<< HEAD
    private BaseEnemyController Controller;
=======

    public IEnumerator StartDamageTimer()
    {
        yield return new WaitForSeconds(AffectDamageTime);

        if (TargetObject != null)
            TargetObject.GetComponent<IDamagable>().TakeDamage(Damage);
    }
>>>>>>> parent of 3dc134a (Merge branch 'eunjin' of https://github.com/HyoJoonLouis/2023_Albal into eunjin)

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
<<<<<<< HEAD

=======
>>>>>>> parent of 3dc134a (Merge branch 'eunjin' of https://github.com/HyoJoonLouis/2023_Albal into eunjin)
        TargetObject = other.gameObject;
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        TargetObject = null;
    }
}
