using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivityTest : MonoBehaviour
{
    private float CameraRotation;
    public Transform CameraTransform;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * PlayerMovement.Sensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * PlayerMovement.Sensitivity * Time.deltaTime;

        CameraRotation += mouseY;
        CameraRotation = Mathf.Clamp(CameraRotation, -90f, 90f);

        CameraTransform.localRotation = Quaternion.Euler(CameraRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseX, 0f));
    }

    public void Reset()
    {
        CameraRotation = 0f;

        CameraTransform.localRotation = Quaternion.Euler(CameraRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
