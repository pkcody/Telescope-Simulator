using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public string name;
    public float size;
    public float distanceFrom;
    public string color;
    public RightAscension rightAscension = new RightAscension();
    public Declination declination = new Declination();
    public string constellation;
}
