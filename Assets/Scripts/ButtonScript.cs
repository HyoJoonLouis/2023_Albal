using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ButtonScript : MonoBehaviour, IPaintable
{
    [SerializeField] float DeactivateTime;
    [SerializeField] GameObject ActivateParticle;
    [SerializeField] Transform ActivateParticlePosition;
    GameObject activateParticle;
    [SerializeField] GameObject SparkParticle;

    [SerializeField] Material ActivateMaterial;
    [SerializeField] Material DeactivateMaterial;
    
    [HideInInspector] public bool isHit;
    [SerializeField] EnergyObject EnergyObject;

    [Header("Sounds")]
    AudioSource audioSource;
    [SerializeField] RandomSounds<AudioClip> ActivateSounds;
    [SerializeField] RandomSounds<AudioClip> OnActivateSounds;



    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Hit()
    {
        if (isHit == true)
            return;

        isHit = true;
        ObjectPoolManager.SpawnObject(SparkParticle, this.transform.position, this.transform.rotation);
        audioSource.PlayOneShot(ActivateSounds.GetRandom());
        audioSource.clip = OnActivateSounds.GetRandom();
        audioSource.Play();
        activateParticle = ObjectPoolManager.SpawnObject(ActivateParticle, ActivateParticlePosition.transform.position, ActivateParticlePosition.transform.rotation);
        this.GetComponent<Renderer>().material = ActivateMaterial;
        Invoke("Deactivate", DeactivateTime);
        if (EnergyObject.CheckButtonsHit())
        {
            EnergyObject?.Activate();
        }
    }

    public void Deactivate()
    {
        isHit = false;
        this.GetComponent<Renderer>().material = DeactivateMaterial;
        ObjectPoolManager.ReturnObjectToPool(activateParticle);
        audioSource.Stop();
        EnergyObject.Deactivate();
    }
}
