using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] RandomSounds<AudioClip> GameOverSounds;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnGameOver()
    {
        GameObject.Find("Camera").GetComponent<AudioSource>().volume = 0.18f;
        audioSource.PlayOneShot(GameOverSounds.GetRandom());
    }
}
