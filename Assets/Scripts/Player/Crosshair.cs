using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    Camera mainCam;
    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 cursorPosition = mainCam.ScreenToWorldPoint(mousePosition);
        transform.position = cursorPosition;
    }
}
