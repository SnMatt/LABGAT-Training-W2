using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
