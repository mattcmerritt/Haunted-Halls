using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject Background;
    public GameObject ReturnToSettingsButton;

    public SensitivityTest SensitivityTest;

    public Slider SensitivitySlider;
    public TMP_Text SensitivityLabel;

    public Slider BrightnessSlider;
    public TMP_Text BrightnessLabel;

    public void UpdateSensitivity()
    {
        PlayerMovement.Sensitivity = SensitivitySlider.value;
        SensitivityLabel.SetText("" + Mathf.Round(PlayerMovement.Sensitivity));
    }

    public void UpdateBrightness()
    {
        Lighting.Brightness = BrightnessSlider.value;
        BrightnessLabel.SetText("" + Lighting.Brightness.ToString("0.##"));
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TestSettings()
    {
        SensitivityTest.Reset();

        Background.SetActive(false);
        ReturnToSettingsButton.SetActive(true);
    }

    public void ReturnToSettings()
    {
        Background.SetActive(true);
        ReturnToSettingsButton.SetActive(false);
    }
}
