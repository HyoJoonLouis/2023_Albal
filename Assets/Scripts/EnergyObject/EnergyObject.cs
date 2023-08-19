using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyObject : MonoBehaviour
{
    ButtonScript[] Buttons;

    public virtual void Start()
    {
        Buttons = GetComponentsInChildren<ButtonScript>();
    }

    public bool CheckButtonsHit()
    {
        foreach (var Button in Buttons)
        {
            if (!Button.isHit)
                return false;
        }
        return true;
    }

    public virtual void Activate()
    {

    }

    public virtual void Deactivate()
    {
    }
}
