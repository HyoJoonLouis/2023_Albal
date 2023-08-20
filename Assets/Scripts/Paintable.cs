using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Paintable : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] Shader materialShader;
    [SerializeField] Texture2D BaseTexture;
    [SerializeField] ComputeShader computeShader;

    [SerializeField] ChangeRenderScript changeRenderScript;

    [HideInInspector] public Material PaintableMaterial;
    public RenderTexture PaintTexture;

    void Start()
    {
        PaintableMaterial = new Material(materialShader);
        PaintTexture = new RenderTexture(BaseTexture.width, BaseTexture.height, 1);
        PaintTexture.enableRandomWrite = true;
        PaintTexture.Create();

        PaintableMaterial.SetTexture("_MainTex", BaseTexture);
        PaintableMaterial.SetTexture("_HitTex", PaintTexture);
        this.GetComponent<Renderer>().material = PaintableMaterial;
    }

    public void Paint(Vector2 HitCoord, Texture2D Brush)
    {
        RenderTexture ResultTexture = new RenderTexture(PaintTexture);
        ResultTexture.enableRandomWrite = true;
        Graphics.CopyTexture(PaintableMaterial.GetTexture("_HitTex"), ResultTexture);
        ResultTexture.Create();

        int kernalId = computeShader.FindKernel("CSMain");
        computeShader.SetTexture(kernalId, "TextureA", PaintableMaterial.GetTexture("_HitTex"));
        computeShader.SetTexture(kernalId, "TextureB", Brush);
        computeShader.SetVector("Position", new Vector2(HitCoord.x * PaintTexture.width, HitCoord.y * PaintTexture.height));
        computeShader.SetTexture(kernalId, "Result", ResultTexture);
        computeShader.Dispatch(kernalId, PaintTexture.width / 8, PaintTexture.height / 8, 1);

        PaintableMaterial.SetTexture("_HitTex", ResultTexture);
/*        this.GetComponent<Renderer>().material = PaintableMaterial;*/
    }
}
