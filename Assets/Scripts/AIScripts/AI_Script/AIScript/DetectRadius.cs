using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRadius : BaseTriggerComp
{
    [Header("Character Detect Enemy Radius")]
    [SerializeField] private float MonsterVisualRadius;
    [Range(0f, 360f)] public float MonsterVisualAngle;

    private Mesh Mesh;
    private MeshFilter DetectMeshFilter;

    private Vector3 RightDir;
    private Vector3 LeftDir;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        DetectMeshFilter = GetComponent<MeshFilter>();
        Mesh = new Mesh();
        Mesh.name = "Radius";
        DetectMeshFilter.mesh = Mesh;
    }

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void Update()
    {
        Vector3 myPos = transform.position + Vector3.up * 0.5f;

        float lookingAngle = transform.eulerAngles.y;

        RightDir = AngleToDir((transform.eulerAngles.y - transform.eulerAngles.y) + MonsterVisualAngle * 0.5f);
        LeftDir = AngleToDir((transform.eulerAngles.y - transform.eulerAngles.y) - MonsterVisualAngle * 0.5f);

        Vector3[] vertices = new Vector3[3];
        int[] triangles = new int[3];


        vertices[0] = Vector3.zero;
        vertices[1] = RightDir * MonsterVisualRadius;
        vertices[2] = LeftDir * MonsterVisualRadius;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        Mesh.vertices = vertices;
        Mesh.triangles = triangles;

    }

    public bool DetectTarget(Vector3 Position)
    {
        Vector2 Dir = (new Vector2(Position.x, Position.z) - new Vector2(transform.position.x, transform.position.z)).normalized;

        if (Vector2.Angle(new Vector2(transform.forward.x * MonsterVisualRadius, transform.forward.z * MonsterVisualRadius), Dir) < MonsterVisualAngle * 0.5f)
        {
            return true;
        }
        return false;
    }
}
