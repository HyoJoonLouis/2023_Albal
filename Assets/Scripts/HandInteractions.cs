using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandInteractions : MonoBehaviour
{
    [Header("Right Hand Actions")]
    [SerializeField] GameObject RightHand;
    [SerializeField] InputActionProperty ShootProperty;
    [SerializeField] InputActionProperty ChangePaintProperty;
    bool isAButtonPressed = false;

    [Header("Left Hand Actions")]
    [SerializeField] InputActionProperty BounceProperty;

    [Header("Adjustments")]
    [SerializeField] Transform ShootPosition;
    [SerializeField] float MaxChargeTime;
    private float ChargeTime;
    [SerializeField] float BulletIncreaseSpeed;
    [SerializeField] float BulletBasicSpeed;
    [SerializeField] float CoolTime;
    private bool isCoolTime;
    [SerializeField] int MaxBulletCount;
    private int currentBulletCount;
    private Material AmmoMaterial;
    private Material TubeMaterial;

    [Header("Right Hand Shake")]
    [SerializeField] float ShakeThreshold;
    [SerializeField] int ShakeAmount;
    [SerializeField] float ShakeCoolTime;
    bool isShakeCoolTime;
    Vector3 PreviousRightHandPosition;
    int CountShake;

    [Header("Paints")]
    [SerializeField]  List<GameObject> PaintBall;
    private int currentPaintIndex;
    
    PaintBall PaintBallInstance;

    [Header("Particles")]
    [SerializeField] GameObject ShootParticle;

    [Header("Sounds")]
    [SerializeField] RandomSounds<AudioClip> ShootSound;
    [SerializeField] RandomSounds<AudioClip> ReloadSound;
    AudioSource audioSource;

    [Header("Goggle")]
    [SerializeField] GoggleScript goggleScript;
    [SerializeField] float GoggleCoolTime;
    [SerializeField] RandomSounds<AudioClip> GoggleOnSounds;
    [SerializeField] RandomSounds<AudioClip> GoggleOffSounds;


    int isTransparent;

    void Start()
    {
        isCoolTime = false;
        ChargeTime = 0;
        currentPaintIndex = 0;
        currentBulletCount = MaxBulletCount;
        PreviousRightHandPosition = RightHand.transform.localPosition;
        CountShake = 0;
        isTransparent = 0;
        AmmoMaterial = GameObject.Find("Sphere002").GetComponent<Renderer>().material;
        TubeMaterial = GameObject.Find("Tube002").GetComponent<Renderer>().material;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("HandShakeDetect");
    }

    // Update is called once per frame
    void Update()
    {
        float ChargeValue = ShootProperty.action.ReadValue<float>();
        float ChangePaintValue = ChangePaintProperty.action.ReadValue<float>();
        float BouncePropertyValue = BounceProperty.action.ReadValue<float>();

        if (ChangePaintValue >= 0.8f && isAButtonPressed == false)
        {
            isAButtonPressed = true;
            Invoke("SetAKeyCoolTime", GoggleCoolTime);
            if (isTransparent == 0)
            {
                isTransparent = 1;
                goggleScript.GoggleOn();
                audioSource.PlayOneShot(GoggleOnSounds.GetRandom());
            }
            else
            {
                isTransparent = 0;
                goggleScript.GoggleOff();
                audioSource.PlayOneShot(GoggleOffSounds.GetRandom());
            }
            Invoke("MakeTransparent", 0.2f);
        }

        if (isCoolTime || currentBulletCount == 0)
            return;

        if(BouncePropertyValue > 0.8f && ChargeTime != 0 && PaintBallInstance != null)
        {
            PaintBallInstance.SetBounce(1);
        }

        if (ChargeValue > 0.8f && ChargeTime == 0 && Time.timeScale == 1.0f)
        {
            PaintBallInstance = ObjectPoolManager.SpawnObject(PaintBall[currentPaintIndex % PaintBall.Count], new Vector3(-9999, -9999, -9999), this.transform.rotation).GetComponent<PaintBall>();
        }
        if (ChargeValue > 0.8f && Time.timeScale == 1.0f)
        {
            ChargeTime = Mathf.Clamp(ChargeTime += Time.unscaledDeltaTime, 0, MaxChargeTime);
            TubeMaterial.SetFloat("_Fill", ChargeTime / MaxChargeTime);
        }
        if (ChargeValue < 0.2f && ChargeTime != 0 && PaintBallInstance != null)
        {
            StartCoroutine(SetCoolTime());
            PaintBallInstance.Init(BulletBasicSpeed + (BulletIncreaseSpeed * ChargeTime), ShootPosition.forward, ShootPosition.position);
            audioSource.PlayOneShot(ShootSound.GetRandom());
            currentBulletCount--;
            AmmoMaterial.SetFloat("_Fill", (float)currentBulletCount / MaxBulletCount);
            ChargeTime = 0;
            TubeMaterial.SetFloat("_Fill", ChargeTime);
            ObjectPoolManager.SpawnObject(ShootParticle, ShootPosition.position, ShootPosition.rotation).transform.SetParent(this.gameObject.transform);
            PaintBallInstance = null;
        }
    }

    private void FixedUpdate()
    {
/*        if (Mathf.Abs(RightHand.transform.localPosition.y - PreviousRightHandPosition.y) >= ShakeThreshold)
        {
            CountShake++;
        }
        else
            CountShake = 0;
        
        if(CountShake > ShakeAmount)
        {
            CountShake = 0;
            currentBulletCount = MaxBulletCount;
            AmmoMaterial.SetFloat("_Fill", 1);
            audioSource.PlayOneShot(ReloadSound.GetRandom());
        }
        PreviousRightHandPosition = RightHand.transform.localPosition;*/
    }
    
    IEnumerator HandShakeDetect()
    {
        if (!isShakeCoolTime)
        {
            if (Mathf.Abs(RightHand.transform.localPosition.y - PreviousRightHandPosition.y) >= ShakeThreshold)
            {
                CountShake++;
            }
            else
                CountShake = 0;

            if (CountShake > ShakeAmount)
            {
                CountShake = 0;
                StartCoroutine("SetShakeCoolTime");
                currentBulletCount = MaxBulletCount;
                AmmoMaterial.SetFloat("_Fill", 1);
                audioSource.PlayOneShot(ReloadSound.GetRandom());
            }
            PreviousRightHandPosition = RightHand.transform.localPosition;
        }
        yield return new WaitForSeconds(0.005f);
        StartCoroutine("HandShakeDetect");
    }

    IEnumerator SetCoolTime()
    {
        Animator animator = RightHand.GetComponent<Animator>();
        isCoolTime = true;
        animator.Play("Reload");
        yield return new WaitForSecondsRealtime(CoolTime);
        animator.Play("Idle");
        isCoolTime = false;
    }

    IEnumerator SetShakeCoolTime()
    {
        isShakeCoolTime = true;
        yield return new WaitForSeconds(ShakeCoolTime);
        isShakeCoolTime = false;
    }

    private void SetAKeyCoolTime()
    {
        isAButtonPressed = false;
    }
    
    private void MakeTransparent()
    {
        GameManager.PaintTransparent(isTransparent);
    }
}