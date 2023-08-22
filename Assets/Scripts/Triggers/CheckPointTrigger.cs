using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
public class CheckPointTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        PlayerCharacter playerCharacter = other.GetComponent<PlayerCharacter>();
        playerCharacter.SetCheckPoint(this.transform.position);
    }

}
