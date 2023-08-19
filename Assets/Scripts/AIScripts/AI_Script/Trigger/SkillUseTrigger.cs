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

    private BaseEnemyController Controller;

    protected override void Awake()
    {
        base.Awake();
        Controller = transform.parent.GetComponent<BaseEnemyController>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
<<<<<<< HEAD
        TargetObject = other.gameObject;
=======
>>>>>>> parent of ab08694 (.)
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        TargetObject = null;
    }
}