using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : MonoBehaviour
{

    private int Bounce;
    private Rigidbody rb;
    private Vector3 PreviousVelocity;

    [Header("Shader")]
    [SerializeField] Texture2D[] PaintBrush;
    [SerializeField] float decreaseSpeedAmount;


    [Header("Particles")]
    [SerializeField] GameObject HitParticle;

    /*    public Texture2D textureA;*/
    private void Start()
    {
        Bounce = 0;
        rb = GetComponent<Rigidbody>();
    }

    public void Init(float speed, Vector3 direction, Vector3 position)
    {
        this.transform.position = position;
/*        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(Speed * Direction, ForceMode.Impulse);*/
        rb.velocity = speed * direction.normalized;
        Debug.Log("Init direction" + direction);
        Debug.Log("Init Velocity" + rb.velocity);
    }
    
    public void SetBounce(int value)
    {
        Bounce = value;
    }

    public void LateUpdate()
    {
        PreviousVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(Bounce > 0)
        {
            Bounce--;
            float speed = PreviousVelocity.magnitude;
            Vector3 direction = Vector3.Reflect(PreviousVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * speed * decreaseSpeedAmount;
            return;
        }

        //Destroy(this.gameObject);
        IPaintable paintable = collision.transform.GetComponent<IPaintable>();
        if (paintable != null)
        {
            paintable.Hit();
        }
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        ObjectPoolManager.SpawnObject(HitParticle, transform.position, transform.rotation);
        Ray ray = new Ray(collision.contacts[0].point + collision.contacts[0].normal , -collision.contacts[0].normal);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,1<<LayerMask.NameToLayer("Paintable")))
        {
            Debug.Log(hit.textureCoord);
            hit.transform.GetComponent<Paintable>().Paint(hit.textureCoord, PaintBrush[Random.Range(0, PaintBrush.Length)]);
        }
    }

    
}
