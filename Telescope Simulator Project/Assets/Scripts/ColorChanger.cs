using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    public Color color;
    public void TryChangeColor(TMP_InputField text)
    {
        string s = text.text;
        if(!s.Contains("#"))
        {
            s = "#" + s;
        }

        if(ColorUtility.TryParseHtmlString(s, out color))
        {
            GetComponent<Image>().color = color;
        }
    }
}
