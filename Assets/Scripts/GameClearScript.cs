using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearScript : MonoBehaviour
{

    [SerializeField] RandomSounds<AudioClip> GameClearSounds;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void OnGameClear()
    {
        audioSource.PlayOneShot(GameClearSounds.GetRandom());
        GameObject.Find("Camera").GetComponent<AudioSource>().volume = 0.18f;
        Invoke("ReturnToMainScene", 30.0f);
    }

    public void ReturnToMainScene()
    {
        SceneManager.LoadScene(0);    
    }
}
