using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerCharacter Player;
    private static Paintable[] paintables;


    void Awake()
    {
        if (Instance == null)
            Instance = this;

        paintables = FindObjectsOfType<Paintable>();
        Player = FindObjectOfType<PlayerCharacter>();
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
        if(Sounds.Count == 0)
        {
            return PreviousSound;
        }

        T UsedSound = Sounds[UnityEngine.Random.Range(0, Sounds.Count)];
        Sounds.Remove(UsedSound);
        if(PreviousSound != null)
            Sounds.Add(PreviousSound);
        PreviousSound = UsedSound;
        return UsedSound;
    }
}