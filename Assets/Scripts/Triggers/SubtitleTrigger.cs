using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
public class SubtitleTrigger : MonoBehaviour
{
    [SerializeField] InputActionProperty PressedActionProperty;

    [SerializeField] bool CanPause;
    [SerializeField] Sprite[] images;
    [SerializeField] AudioClip[] audioClips;

    int index;
    bool isPressed;
    bool isTriggered;
    AudioSource audioSource;

    private Image Subtitle;
    public void Start()
    {
        Subtitle = GameObject.Find("Subtitle").GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        index = 0;
        isTriggered = false;
        isPressed = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(CanPause)
        {
            Time.timeScale = 0;
        }
        isTriggered = true;
        Subtitle.color = new Color(255, 255, 255, 255);
        Subtitle.sprite = images[index];
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }

    public void Update()
    {
        if(!isTriggered)
            return;

        float PressedValue = PressedActionProperty.action.ReadValue<float>();
        if (PressedValue >= 0.8 && isPressed == false)
        {
            isPressed = true;
            if (++index >= images.Length)
            {
                Time.timeScale = 1;
                Subtitle.color = new Color(0, 0, 0, 0);
                this.gameObject.SetActive(false);
                return;
            }
            Subtitle.sprite = images[index];
            audioSource.clip = audioClips[index];
            audioSource.Play();

        }
        if (PressedValue <= 0.2f && isPressed == true)
            isPressed = false;

    }

}
