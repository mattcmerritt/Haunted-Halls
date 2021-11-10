using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        FindObjectOfType<ActiveSceneManager>().SwitchScenes(1);
    }

    public void LoadSettings()
    {
        FindObjectOfType<ActiveSceneManager>().SwitchScenes(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}