using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
public class SubtitleTrigger : MonoBehaviour
{
    [SerializeField] InputActionProperty RIghtHandAKey;

    [SerializeField] bool CanPause;
    [SerializeField] string Text;

    private Text Subtitle;
    public void Start()
    {
        Subtitle = GameObject.Find("Subtitle").GetComponent<Text>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(CanPause)
        {
            EditorApplication.isPaused = true;
        }
        Subtitle.text = Text;
    }

    public void OnTriggerExit(Collider other)
    {
        Subtitle.text = "";
    }


    public void Update()
    {
        bool AKeyPressed = RIghtHandAKey.action.ReadValue<bool>();
        if(AKeyPressed && EditorApplication.isPaused == true)
        {
            EditorApplication.isPaused = false;
        }
    }
}
