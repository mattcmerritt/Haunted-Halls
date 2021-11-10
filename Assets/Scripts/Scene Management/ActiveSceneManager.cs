using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSceneManager : MonoBehaviour
{
    public List<GameObject> Scenes;

    public void SwitchScenes(int index)
    {
        for (int i = 0; i < Scenes.Count; i++)
        {
            Scenes[i].SetActive(i == index);
        }

        if (index == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
