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


    public IEnumerator StartDamageTimer()
    {
        yield return new WaitForSeconds(AffectDamageTime);

        if (TargetObject != null)
            TargetObject.GetComponent<IDamagable>().TakeDamage(Damage);
    }

    private BaseEnemyController Controller;

    protected override void Awake()
    {
        base.Awake();
        Controller = transform.parent.GetComponent<BaseEnemyController>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
<<<<<<< Updated upstream
        TargetObject = other.gameObject;
=======
        Controller.PlayerDamagable = other.GetComponent<IDamagable>();
>>>>>>> Stashed changes
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
<<<<<<< Updated upstream
        TargetObject = null;
=======
        Controller.PlayerDamagable = null;
>>>>>>> Stashed changes
    }
}