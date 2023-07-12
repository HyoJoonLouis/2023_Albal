using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHandInteractions : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] InputActionProperty ShootProperty;
    [SerializeField] InputActionProperty ChangePaint;

    [Header("Adjustments")]
    [SerializeField] float MaxChargeTime;
    private float ChargeTime;
    [SerializeField] float BulletIncreaseSpeed;
    [SerializeField] float BulletBasicSpeed;


    [Header("Paints")]
    [SerializeField] GameObject PaintBall;

    void Start()
    {
        ChargeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float ChargeValue = ShootProperty.action.ReadValue<float>();
        if (ChargeValue > 0.8f)
        {
            ChargeTime = Mathf.Clamp(ChargeTime += Time.deltaTime, 0, MaxChargeTime);
        }
        else if (ChargeValue < 0.1f && ChargeTime != 0)
        {
            GameObject paintBall = Instantiate(PaintBall, this.transform.position, this.transform.rotation);
            paintBall.GetComponent<PaintBall>().Init(BulletBasicSpeed + (BulletIncreaseSpeed * ChargeTime), this.transform.forward);
            ChargeTime = 0;
        }
      
    }
}
