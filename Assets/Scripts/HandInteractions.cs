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
    Vector3 PreviousRightHandPosition;
    int CountShake;

    [Header("Paints")]
    [SerializeField]  List<GameObject> PaintBall;
    private int currentPaintIndex;
    
    PaintBall PaintBallInstance;

    int isTransparent;

    void Start()
    {
        isCoolTime = false;
        ChargeTime = 0;
        currentPaintIndex = 0;
        currentBulletCount = MaxBulletCount;
        PreviousRightHandPosition = RightHand.transform.position;
        CountShake = 0;
        isTransparent = 0;
        AmmoMaterial = GameObject.Find("Sphere002").GetComponent<Renderer>().material;
        TubeMaterial = GameObject.Find("Tube002").GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float ChargeValue = ShootProperty.action.ReadValue<float>();
        float ChangePaintValue = ChangePaintProperty.action.ReadValue<float>();
        float BouncePropertyValue = BounceProperty.action.ReadValue<float>();

        if (ChangePaintValue > 0.8f)
        {
            if(isTransparent == 0)
                isTransparent = 1;
            else
                isTransparent = 0;
            GameManager.PaintTransparent(isTransparent);
        }

        if (isCoolTime || currentBulletCount == 0)
            return;

        if(BouncePropertyValue > 0.8f && ChargeTime != 0)
        {
            PaintBallInstance.SetBounce(1);
        }

        if (ChargeValue > 0.8f && ChargeTime == 0)
        {
            PaintBallInstance = ObjectPoolManager.SpawnObject(PaintBall[currentPaintIndex % PaintBall.Count], new Vector3(-9999, 0, 0), this.transform.rotation).GetComponent<PaintBall>();
/*            PaintBallInstance = Instantiate(PaintBall[currentPaintIndex % PaintBall.Count], new Vector3(0,0,-9999), this.transform.rotation).GetComponent<PaintBall>();*/
        }
        if (ChargeValue > 0.8f)
        {
            ChargeTime = Mathf.Clamp(ChargeTime += Time.deltaTime, 0, MaxChargeTime);
            TubeMaterial.SetFloat("_Fill", ChargeTime / MaxChargeTime);
        }
        if (ChargeValue < 0.2f && ChargeTime != 0)
        {
            PaintBallInstance.Init(BulletBasicSpeed + (BulletIncreaseSpeed * ChargeTime), ShootPosition.forward, ShootPosition.position);
            currentBulletCount--;
            Debug.Log(currentBulletCount / MaxBulletCount);
            AmmoMaterial.SetFloat("_Fill", (float)currentBulletCount / MaxBulletCount);
            ChargeTime = 0;
            TubeMaterial.SetFloat("_Fill", 0);
            PaintBallInstance = null;
            StartCoroutine(SetCoolTime());
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(RightHand.transform.position, PreviousRightHandPosition) >= ShakeThreshold)
        {
            CountShake++;
            Debug.Log("Shaked");
        }
        else
            CountShake = 0;
        
        if(CountShake > ShakeAmount)
        {
            Debug.Log("All Shaked");
            CountShake = 0;
            currentBulletCount = MaxBulletCount;
            AmmoMaterial.SetFloat("_Fill", 1);
        }
        PreviousRightHandPosition = RightHand.transform.position;
    }

    IEnumerator SetCoolTime()
    {
        isCoolTime = true;
        yield return new WaitForSecondsRealtime(CoolTime);
        isCoolTime = false;
    }
}