using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessLighting : MonoBehaviour
{
    public PostProcessVolume Volume;
    public ColorGrading Grading;
    public static float Brightness = 0f;

    private void Start()
    {
        Volume.profile.TryGetSettings(out Grading);
        UpdateLighting();
    }

    public void UpdateLighting()
    {
        Grading.brightness.value = Brightness;
    }
}
