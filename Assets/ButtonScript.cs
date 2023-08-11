using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour, IPaintable
{
    [SerializeField] float DeactivateTime;

    [HideInInspector]public bool isHit;
    EnergyObject EnergyObject;

    public void Start()
    {
        EnergyObject = GetComponentInParent<EnergyObject>();
    }
    public void Hit()
    {
        isHit = true;
        Invoke("Deactivate", DeactivateTime);
        if (EnergyObject.CheckButtonsHit())
        {
            EnergyObject.Activate();
        }
    }

    public void Deactivate()
    {
        isHit = false;
        EnergyObject.Deactivate();
    }
}
