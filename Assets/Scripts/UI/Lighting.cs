using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public static float Brightness;

    private void Update()
    {
        RenderSettings.reflectionIntensity = Brightness;
    }
}
