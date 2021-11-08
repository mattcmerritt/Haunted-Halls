using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public static float Brightness = 0.15f;

    private void Start()
    {
        UpdateLighting();
    }

    public void UpdateLighting()
    {
        Debug.Log("Before: " + RenderSettings.reflectionIntensity);
        RenderSettings.reflectionIntensity = Brightness;
        Debug.Log("After: " + RenderSettings.reflectionIntensity);
    }
}
