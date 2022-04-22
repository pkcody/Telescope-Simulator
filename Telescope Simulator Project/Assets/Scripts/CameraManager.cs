using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraManager : MonoBehaviour
{
    public Camera mc;
    public bool isFreeLook = true;

    void Start()
    {
        mc = Camera.main;
        mc.usePhysicalProperties = true;
        //adjustCamera(20, 150, 650);
    }

    void adjustCamera(float eyepieceFOV, float magnification, float focalLength)
    {
        //mc.fieldOfView = (eyepieceFOV / magnification);
        mc.focalLength += focalLength;
        mc.sensorSize = Vector2.one / 10;
    }

    public void dropDownCameraAdjust(TMP_Dropdown focalLength)
    {
        string s = focalLength.options[focalLength.value].text;

        if(s == "Free Look")
        {
            isFreeLook = true;
            return;
        }
        else
        {
            isFreeLook = false;
        }

        s = s.Replace("mm", "");

        print(s);

        float fl = float.Parse(s);

        mc.focalLength = fl;
    }

    private void Update()
    {
        if(isFreeLook)
        {
            if (!(Input.mouseScrollDelta == Vector2.zero))
            {
                mc.fieldOfView += Input.mouseScrollDelta.y * -1 / 10;
            }
        }
        
    }
}
