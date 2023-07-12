using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHandInteractions : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] InputActionProperty ShootProperty;
    [SerializeField] InputActionProperty ChangePaintProperty;
    [SerializeField] InputActionProperty BounceProperty;

    [Header("Adjustments")]
    [SerializeField] float MaxChargeTime;
    private float ChargeTime;
    [SerializeField] float BulletIncreaseSpeed;
    [SerializeField] float BulletBasicSpeed;
    [SerializeField] float CoolTime;
    private bool isCoolTime;

    [Header("Paints")]
    [SerializeField]  List<GameObject> PaintBall;
    private int currentPaintIndex;
    

    PaintBall PaintBallInstance;



    void Start()
    {
        ChargeTime = 0;
        isCoolTime = false;
        currentPaintIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float ChargeValue = ShootProperty.action.ReadValue<float>();
        float ChangePaintValue = ChangePaintProperty.action.ReadValue<float>();
        float BouncePropertyValue = BounceProperty.action.ReadValue<float>();

        if (isCoolTime)
            return;

        if (ChangePaintValue > 0.8f)
        {
            currentPaintIndex++;
        }

        if (ChargeValue > 0.8f)
        {
            ChargeTime = Mathf.Clamp(ChargeTime += Time.deltaTime, 0, MaxChargeTime);
            PaintBallInstance = Instantiate(PaintBall[currentPaintIndex % PaintBall.Count], this.transform.position, this.transform.rotation).GetComponent<PaintBall>();
        }
        else if(BouncePropertyValue > 0.8f && ChargeTime != 0)
        {
            PaintBallInstance.SetBounce(1);
        }
        else if (ChargeValue < 0.1f && ChargeTime != 0)
        {
            PaintBallInstance.Init(BulletBasicSpeed + (BulletIncreaseSpeed * ChargeTime), this.transform.forward, this.transform.position);
            ChargeTime = 0;
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
