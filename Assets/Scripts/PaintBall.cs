using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : MonoBehaviour
{
    private float Speed;
    private Vector3 Direction;
    private int Bounce;

    [Header("Shader")]
    [SerializeField] Texture2D PaintBrush;


/*    public Texture2D textureA;*/
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = Direction * Speed * Time.deltaTime;
    }

    public void Init(float speed, Vector3 direction, Vector3 position)
    {
        this.transform.position = position;
        Speed = speed;
        Direction = direction.normalized;
        Bounce = 0;
    }
    
    public void SetBounce(int value)
    {
        Bounce = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == this || !collision.transform.CompareTag("Paintable")) return;

        if(Bounce > 0)
        {
            Bounce--;
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.velocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
            return;
        }

        Destroy(this.gameObject);

        Ray ray = new Ray(collision.contacts[0].point, - collision.contacts[0].normal);
        if (Physics.Raycast(ray, out RaycastHit hit, 1 << LayerMask.NameToLayer("Paintable")))
        {
            Debug.Log(hit.textureCoord);
            hit.transform.GetComponent<Paintable>().Paint(hit.textureCoord, PaintBrush);
/*            Material hitMaterial = hit.transform.GetComponent<Renderer>().material;
            textureA = hitMaterial.GetTexture("_MainTex") as Texture2D;
            resultTexture = new RenderTexture(textureA.width, textureA.height, 1);
            resultTexture.enableRandomWrite = true;
            resultTexture.Create();

            int kernalId = computeShader.FindKernel("CSMain");
            computeShader.SetTexture(kernalId, "Result", resultTexture);
            computeShader.SetTexture(kernalId, "TextureA", textureA);
            computeShader.SetTexture(kernalId, "TextureB", Paint);
            computeShader.SetVector("Position", new Vector2(hit.textureCoord.x * textureA.width, hit.textureCoord.y * textureA.height));
            computeShader.Dispatch(kernalId, Paint.width / 8, Paint.height / 8, 1);
            hitMaterial.SetTexture("_HitTex", resultTexture);*/
        }
    }
}
