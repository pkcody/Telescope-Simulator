using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string name;
    public float size;
    public float distanceFrom;
    public RightAscension rightAscension = new RightAscension();
    public Declination declination = new Declination();
}
