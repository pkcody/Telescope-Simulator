using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mc;

    void Start()
    {
        mc = Camera.main;
        mc.usePhysicalProperties = true;
        //adjustCamera(20, 150, 650);
    }

    void adjustCamera(float eyepieceFOV, float magnification, float focalLength)
    {
        mc.fieldOfView = (eyepieceFOV / magnification);
        mc.focalLength += focalLength;
        mc.sensorSize = Vector2.one / 10;
    }

    private void Update()
    {
        if(!(Input.mouseScrollDelta == Vector2.zero))
        {
            mc.fieldOfView += Input.mouseScrollDelta.y * -1;
        }
    }
}
