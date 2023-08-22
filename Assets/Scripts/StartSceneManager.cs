using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{

    [SerializeField] Animator animator;
    public void OnStartButton()
    {
        animator.Play("FadeIn");
    }

    public void OnOptionButton()
    {

    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

}
