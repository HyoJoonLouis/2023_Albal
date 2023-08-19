using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetectScript : MonoBehaviour
{
    public GameObject Target;

    public void OnTriggerEnter(Collider other)
    {
        Target = other.gameObject;
    }

    public void OnTriggerExit(Collider other)
    {
        Target = null;
    }
}
