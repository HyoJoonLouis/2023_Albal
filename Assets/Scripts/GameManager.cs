using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Paintable[] paintables;


    void Start()
    {
        paintables = FindObjectsOfType<Paintable>();
    }

    public static void PaintTransparent(int isTransparent)
    {

        foreach (Paintable paintable in paintables)
        {
            paintable.PaintableMaterial.SetInt("_IsTransparent", isTransparent);
        }
    }
}
