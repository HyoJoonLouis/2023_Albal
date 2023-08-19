using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ButtonScript : MonoBehaviour, IPaintable
{
    [SerializeField] float DeactivateTime;
    [SerializeField] GameObject ActivateParticle;
    GameObject activateParticle;
    [SerializeField] GameObject SparkParticle;

    [SerializeField] Material ActivateMaterial;
    [SerializeField] Material DeactivateMaterial;
    
    [HideInInspector] public bool isHit;
    [SerializeField] EnergyObject EnergyObject;


    public void Hit()
    {
        if (isHit == true)
            return;

        isHit = true;
        ObjectPoolManager.SpawnObject(SparkParticle, this.transform.position, this.transform.rotation);
        activateParticle = ObjectPoolManager.SpawnObject(ActivateParticle, this.transform.position, this.transform.rotation);
        this.GetComponent<Renderer>().material = ActivateMaterial;
        Invoke("Deactivate", DeactivateTime);
        if (EnergyObject.CheckButtonsHit())
        {
            EnergyObject?.Activate();
        }
    }

    public void Deactivate()
    {
        isHit = false;
        this.GetComponent<Renderer>().material = DeactivateMaterial;
        ObjectPoolManager.ReturnObjectToPool(activateParticle);
        EnergyObject.Deactivate();
    }
}
