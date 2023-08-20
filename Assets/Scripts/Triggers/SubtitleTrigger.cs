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
    AudioSource audioSource;

    private Image Subtitle;
    public void Start()
    {
        Subtitle = GameObject.Find("Subtitle").GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        index = 0;
        isPressed = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(CanPause)
        {
            Time.timeScale = 0;
        }
        Subtitle.color = new Color(255, 255, 255, 255);
        Subtitle.sprite = images[index];
        audioSource.PlayOneShot(audioClips[index]);
    }

    public void Update()
    {
        float PressedValue = PressedActionProperty.action.ReadValue<float>();
        if (PressedValue >= 0.8 && isPressed == false)
            isPressed = true;

        if(isPressed)
        {
            if(++index > images.Length)
            {
                Time.timeScale = 1;
                Subtitle.color = new Color(0, 0, 0, 0);
                this.gameObject.SetActive(false);
                return;
            }
            Subtitle.sprite = images[index];
            audioSource.PlayOneShot(audioClips[index]);
            isPressed = false;
        }
    }

}
