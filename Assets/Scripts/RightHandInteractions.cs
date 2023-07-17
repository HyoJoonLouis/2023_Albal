using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHandInteractions : MonoBehaviour
{
    [Header("Right Hand Actions")]
    [SerializeField] InputActionProperty ShootProperty;
    [SerializeField] InputActionProperty ChangePaintProperty;

    [Header("Left Hand Actions")]
    [SerializeField] InputActionProperty BounceProperty;

    [Header("Adjustments")]
    [SerializeField] float MaxChargeTime;
    private float ChargeTime;
    [SerializeField] float BulletIncreaseSpeed;
    [SerializeField] float BulletBasicSpeed;
    [SerializeField] float CoolTime;
    private bool isCoolTime;
    [SerializeField] int MaxBulletCount;
    private int currentBulletCount;

    [Header("Paints")]
    [SerializeField]  List<GameObject> PaintBall;
    private int currentPaintIndex;
    
    PaintBall PaintBallInstance;

    void Start()
    {
        isCoolTime = false;
        ChargeTime = 0;
        currentPaintIndex = 0;
        currentBulletCount = MaxBulletCount;
    }

    // Update is called once per frame
    void Update()
    {
        float ChargeValue = ShootProperty.action.ReadValue<float>();
        float ChangePaintValue = ChangePaintProperty.action.ReadValue<float>();
        float BouncePropertyValue = BounceProperty.action.ReadValue<float>();

        if (isCoolTime || currentBulletCount == 0)
            return;

        if (ChangePaintValue > 0.8f)
        {
            currentPaintIndex++;
        }
        if(BouncePropertyValue > 0.8f && ChargeTime != 0)
        {
            PaintBallInstance.SetBounce(1);
        }

        if (ChargeValue > 0.8f && ChargeTime == 0)
        {
            PaintBallInstance = Instantiate(PaintBall[currentPaintIndex % PaintBall.Count], new Vector3(0,0,-9999), this.transform.rotation).GetComponent<PaintBall>();
        }
        if (ChargeValue > 0.8f)
        {
            ChargeTime = Mathf.Clamp(ChargeTime += Time.deltaTime, 0, MaxChargeTime);
        }
        if (ChargeValue < 0.2f && ChargeTime != 0)
        {
            PaintBallInstance.Init(BulletBasicSpeed + (BulletIncreaseSpeed * ChargeTime), this.transform.forward, this.transform.position);
            currentBulletCount--;
            ChargeTime = 0;
            PaintBallInstance = null;
            StartCoroutine(SetCoolTime());
        }
    }

    IEnumerator SetCoolTime()
    {
        isCoolTime = true;
        yield return new WaitForSecondsRealtime(CoolTime);
        isCoolTime = false;
    }
}