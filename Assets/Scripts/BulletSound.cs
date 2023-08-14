using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletSound : MonoBehaviour
{
    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();    
    }
    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
            return;
        }
        audioSource.PlayOneShot(audioClip);
        Invoke("ReturnObjectToPool", audioClip.length + 0.2f);
    }

    private void ReturnObjectToPool()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
