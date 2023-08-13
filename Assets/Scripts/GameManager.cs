using System;
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

[Serializable]
public class RandomSounds<T>
{
    public List<T> Sounds;

    T PreviousSound;

    public T GetRandom()
    {
        T UsedSound = Sounds[UnityEngine.Random.Range(0, Sounds.Count)];
        Sounds.Remove(UsedSound);
        Sounds.Add(PreviousSound);
        PreviousSound = UsedSound;
        return UsedSound;
    }
}