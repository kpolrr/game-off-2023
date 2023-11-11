using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float sensX;
    public float sensY;

    float rotationX;
    float rotationY;

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -15f, 35f);

        rotationY += mouseX;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
