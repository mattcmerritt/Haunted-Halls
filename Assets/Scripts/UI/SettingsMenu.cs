using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public Slider SensitivitySlider;
    public TMP_Text SensitivityLabel;

    public void UpdateSensitivity()
    {
        PlayerMovement.Sensitivity = SensitivitySlider.value;
        SensitivityLabel.SetText("" + Mathf.Round(PlayerMovement.Sensitivity));
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
