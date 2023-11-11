using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float sensX;
    public float sensY;

    float rotationX;
    float rotationY;

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;

        rotationY += mouseX;

        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
