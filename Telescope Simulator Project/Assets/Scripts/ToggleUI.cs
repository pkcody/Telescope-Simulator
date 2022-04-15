using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public GameObject UIContainer;

    public void ToggleUIBtn()
    {
        UIContainer.SetActive(!UIContainer.activeInHierarchy);
        
        if(!UIContainer.activeInHierarchy)
        {
            gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
        }
        else
        {
            gameObject.transform.localEulerAngles = Vector3.zero;
        }
    }
}
