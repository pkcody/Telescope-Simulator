using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnableSubmitBtn : MonoBehaviour
{
    public List<TMP_InputField> iFields;
    public Button submitBtn;
    private void Start()
    {
        submitBtn.interactable = false;
    }

    public void TryEnableSubmitBtn()
    {
        foreach(TMP_InputField i in iFields)
        {
            if(i.text == "")
            {
                return;
            }
        }

        submitBtn.interactable = true;
    }
}
