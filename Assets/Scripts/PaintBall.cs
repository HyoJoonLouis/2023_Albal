using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : MonoBehaviour
{

    private int Bounce;
    private Rigidbody rb;
    private Vector3 PreviousVelocity;

    [Header("Damage")]
    [SerializeField] float Damage;
    [SerializeField] GameObject SplashDamageObject;

    [Header("Shader")]
    [SerializeField] Texture2D[] PaintBrush;
    [SerializeField] float decreaseSpeedAmount;

    [Header("Particles")]
    [SerializeField] GameObject HitParticle;
    [SerializeField] GameObject TrailParticle;
    GameObject SpawnedTrail;

    [Header("Sounds")]
    [SerializeField] GameObject BulletSoundInstance;
    [SerializeField] RandomSounds<AudioClip> ExplodeSound;
    AudioSource audioSource;

    /*    public Texture2D textureA;*/
    private void Awake()
    {
        Bounce = 0;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,0);
        audioSource = GetComponent<AudioSource>();
    }

    public void Init(float speed, Vector3 direction, Vector3 position)
    {
        this.transform.position = position;
/*        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(Speed * Direction, ForceMode.Impulse);*/
        rb.velocity = speed * direction.normalized;
        SpawnedTrail = ObjectPoolManager.SpawnObject(TrailParticle, this.transform.position, this.transform.rotation);
        SpawnedTrail.transform.SetParent(this.transform);
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
        if (collision.transform.CompareTag("Player"))
            return;

        if (Bounce > 0)
        {
            Bounce--;
            float speed = PreviousVelocity.magnitude;
            Vector3 direction = Vector3.Reflect(PreviousVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * speed * decreaseSpeedAmount;
            return;
        }

        Debug.Log(collision.transform.name);
        ObjectPoolManager.SpawnObject(SplashDamageObject, this.transform.position, this.transform.rotation);
        ObjectPoolManager.SpawnObject(HitParticle, transform.position, transform.rotation);
        ObjectPoolManager.SpawnObject(BulletSoundInstance, transform.position, transform.rotation).GetComponent<BulletSound>().PlaySound(ExplodeSound.GetRandom());
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        
        IPaintable paintable = collision.transform.GetComponent<IPaintable>();
        if (paintable != null)
        {
            paintable.Hit();
        }

        Ray ray = new Ray(collision.contacts[0].point + collision.contacts[0].normal, -collision.contacts[0].normal);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.1f, 1 << LayerMask.NameToLayer("Paintable")))
        {
            Debug.Log(hit.textureCoord);
            hit.transform.GetComponent<Paintable>().Paint(hit.textureCoord, PaintBrush[Random.Range(0, PaintBrush.Length)]);
        }
    }    
}
