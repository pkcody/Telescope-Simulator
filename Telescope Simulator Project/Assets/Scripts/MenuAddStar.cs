using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;
using System;

public class MenuAddStar : MonoBehaviour
{
    public bool UseExternalDB = false;
    string externalDomain = "https://unityxampptutorial.000webhostapp.com/";
    string urlHeader;

    public TMP_InputField name_IF;
    public TMP_InputField size_IF;
    public TMP_InputField distanceFrom_IF;
    public TMP_InputField color_IF;
    public TMP_InputField rightAsc_IF;
    public TMP_InputField declin_IF;
    public TMP_InputField constell_IF;
    public TMP_InputField addedBy_IF;

    private void Start()
    {
        urlHeader = externalDomain;
    }

    public void AddStarBtn()
    {
        StartCoroutine(AddStar());
    }

    public IEnumerator AddStar()
    {
        string uri = urlHeader + "AddStar.php";
        WWWForm form = new WWWForm();

        form.AddField("Name", name_IF.text);
        form.AddField("Size", size_IF.text);
        form.AddField("DistanceFrom", distanceFrom_IF.text);
        form.AddField("Color", color_IF.text);
        form.AddField("RightAscension", rightAsc_IF.text);
        form.AddField("Declination", declin_IF.text);
        form.AddField("Constellation", constell_IF.text);
        form.AddField("AddedBy", addedBy_IF.text);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
