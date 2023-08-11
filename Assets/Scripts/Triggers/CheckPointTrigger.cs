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
        PlayerCharacter playerCharacter = GetComponent<PlayerCharacter>();
        if(other.transform.CompareTag("Player") && playerCharacter != null)
        {
            playerCharacter.SetCheckPoint(this.transform);
        }
    }

}
