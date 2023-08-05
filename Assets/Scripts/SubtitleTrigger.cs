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
        if (!other.CompareTag("Player"))
            return;

        if(CanPause)
        {
            Time.timeScale = 0.0f;
        }
        Subtitle.text = Text;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        Subtitle.text = "";
    }


    public void Update()
    {
        bool AKeyPressed = RIghtHandAKey.action.ReadValue<bool>();
        if(AKeyPressed && Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
    }
}
