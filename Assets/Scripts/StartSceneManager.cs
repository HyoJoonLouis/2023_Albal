using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void OnStartButton()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = true;

    }

    public void OnOptionButton()
    {

    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
