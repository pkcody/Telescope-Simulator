using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPlacer : MonoBehaviour
{
    //Transform
    public Transform t;

    //right ascension
    int hour;
    int minutes;
    int seconds;

    float decimalFromLongitude;

    //declination
    float degrees;

    float decimalFromLatitude;

    public float Convert()
    {
        float temp = hour + minutes / 60 + seconds / 3600;

        temp = temp * 15;

        if (temp > 180)
        {
            return temp - 360;
        }
        else
        {
            return temp;
        }
    }

    public void StarShoot(int aH, int aM, int aS, int dD, int dM, int dS, float StarScale, float StarDistance)
    {
        hour = aH;
        minutes = aM;
        seconds = aS;
        decimalFromLongitude = Convert();

        hour = dD;
        minutes = dM;
        seconds = dS;

        decimalFromLatitude = Convert();

        t.rotation = Quaternion.Euler(decimalFromLongitude, decimalFromLatitude, 0);

        GameObject star;

        star = (GameObject)Instantiate(Resources.Load("Prefabs/Star"), t, false);
        star.transform.position = Vector3.forward * StarDistance;
        star.transform.localScale = Vector3.one * StarScale;

    }

    private void Start()
    {
        StarShoot(21, 45, 55, -14, 49, 40, .27f, 17884.43f);
    }


}
