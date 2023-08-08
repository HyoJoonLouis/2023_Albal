using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
public class SubtitleTrigger : MonoBehaviour
{
    [SerializeField] InputActionProperty PressedActionProperty;

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
            Time.timeScale = 0;
        }
        Subtitle.text = Text;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        Subtitle.text = "";
        this.gameObject.SetActive(false);
    }


    public void Update()
    {
        float isPressed = PressedActionProperty.action.ReadValue<float>();
        if (isPressed >= 0.8 && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

}
