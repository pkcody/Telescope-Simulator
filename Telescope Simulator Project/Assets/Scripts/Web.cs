using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;
using System;

public class Web : MonoBehaviour
{
    public bool UseExternalDB = false;
    //string internalDomain = "http://localhost/UnityBackendTutorial/";
    string externalDomain = "https://unityxampptutorial.000webhostapp.com/";
    string urlHeader;

    public List<Star> starPanels;
    public List<Planet> planetPanels;

    public List<GameObject> stars;
    public List<GameObject> planets;

    public TMP_Dropdown magDropDown;
    public TMP_Dropdown apDropDown;

    public List<string> magnifcations;
    public List<string> apertures;

    void Start()
    {
        urlHeader = externalDomain;

        StartCoroutine(GetStars());
        StartCoroutine(GetPlanets());
        StartCoroutine(GetTelescopeSpecs());
    }

    public IEnumerator GetStars()
    {
        string uri = urlHeader + "GetStars.php";
        WWWForm form = new WWWForm();

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
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
                    string jsonArray = webRequest.downloadHandler.text;
                    splitStarInfo(jsonArray);
                    break;
            }
        }
    }
    public void splitStarInfo(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;
        
        for (int i = 0; i < jsonArray.Count; i++)
        {
            string name = jsonArray[i].AsObject["Name"];
            float size = jsonArray[i].AsObject["Size"];
            float distanceFrom = jsonArray[i].AsObject["DistanceFrom"];
            string color = jsonArray[i].AsObject["Color"];
            string rightAsc = jsonArray[i].AsObject["RightAscension"];
            string declin = jsonArray[i].AsObject["Declination"];
            string constell = jsonArray[i].AsObject["Constellation"];

            #region Star Panel
            GameObject starPanel = Instantiate(Resources.Load("StarPanel") as GameObject);
            Star star = starPanel.AddComponent<Star>();

            starPanel.name = name + "Panel";

            star.name = name;
            star.size = size;
            star.distanceFrom = distanceFrom;
            star.color = color;

            string[] rA = rightAsc.Split(' ');
            star.rightAscension.hours = float.Parse(rA[0]);
            star.rightAscension.min = float.Parse(rA[1]);
            star.rightAscension.sec = float.Parse(rA[2]);

            string[] dec = declin.Split(' ');
            star.declination.degrees = float.Parse(dec[0]);
            star.declination.minOfArc = float.Parse(dec[1]);
            star.declination.secOfArc = float.Parse(dec[2]);

            star.constellation = constell;

            starPanel.transform.SetParent(FindObjectOfType<Canvas>().transform.Find("Stars").transform, false);
            starPanel.transform.localScale = new Vector3(1.646302f, 1.646302f, 1.646302f);

            starPanel.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
            starPanel.transform.Find("Constellation").GetComponent<TextMeshProUGUI>().text = "Constellation: " + constell;
            starPanel.transform.Find("Size").GetComponent<TextMeshProUGUI>().text = "Solar Radius: " + size.ToString();
            starPanel.transform.Find("DistanceFrom").GetComponent<TextMeshProUGUI>().text = "Distance From Sun: " + distanceFrom.ToString() + " ly";
            starPanel.transform.Find("Color").GetComponent<TextMeshProUGUI>().text = "Color: " + color; 
            starPanel.transform.Find("RightAscension").GetComponent<TextMeshProUGUI>().text = "Right Ascension: " +  rightAsc;
            starPanel.transform.Find("Declination").GetComponent<TextMeshProUGUI>().text = "Declination: " + declin;
            #endregion
            #region Star GameObject

            GameObject starGO = Instantiate(Resources.Load("StarGO") as GameObject);
            starGO.name = name;

            Color c;
            if (!color.Contains("#"))
            {
                color = "#" + color;
            }
            if (ColorUtility.TryParseHtmlString(color, out c))
            {
                starGO.GetComponent<MeshRenderer>().material.color = c;
            }
            
            starGO.transform.localPosition = new Vector3(star.declination.degrees, star.declination.minOfArc, star.declination.secOfArc);
            starGO.transform.SetParent(GameObject.Find("StarGOs").transform, false);

            #endregion

            starPanel.SetActive(false);

            starGO.GetComponent<StarGO>().myStarPanel = starPanel.GetComponent<Star>();

            starPanels.Add(star);
            stars.Add(starGO);
        }
    }

    public IEnumerator GetPlanets()
    {
        string uri = urlHeader + "GetPlanets.php";
        WWWForm form = new WWWForm();

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
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
                    string jsonArray = webRequest.downloadHandler.text;
                    splitPlanetInfo(jsonArray);
                    break;
            }
        }
    }
    public void splitPlanetInfo(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            string name = jsonArray[i].AsObject["Name"];
            float size = jsonArray[i].AsObject["Size"];
            float distanceFrom = jsonArray[i].AsObject["DistanceFrom"];
            string rightAsc = jsonArray[i].AsObject["RightAscension"];
            string declin = jsonArray[i].AsObject["Declination"];

            #region Planet Panel
            GameObject planetPanel = Instantiate(Resources.Load("PlanetPanel") as GameObject);
            Planet planet = planetPanel.AddComponent<Planet>();

            planetPanel.name = name + "Panel";

            planet.name = name;
            planet.size = size;
            planet.distanceFrom = distanceFrom;

            string[] rA = rightAsc.Split(' ');
            planet.rightAscension.hours = float.Parse(rA[0]);
            planet.rightAscension.min = float.Parse(rA[1]);
            planet.rightAscension.sec = float.Parse(rA[2]);

            string[] dec = declin.Split(' ');
            planet.declination.degrees = float.Parse(dec[0]);
            planet.declination.minOfArc = float.Parse(dec[1]);
            planet.declination.secOfArc = float.Parse(dec[2]);

            planetPanel.transform.SetParent(FindObjectOfType<Canvas>().transform.Find("Planets").transform, false);
            planetPanel.transform.localScale = new Vector3(1.646302f, 1.646302f, 1.646302f);

            planetPanel.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
            planetPanel.transform.Find("Size").GetComponent<TextMeshProUGUI>().text = "Radius:" + size.ToString() + " miles";
            planetPanel.transform.Find("DistanceFrom").GetComponent<TextMeshProUGUI>().text = "Distance From Sun: " + distanceFrom.ToString() + " mil miles";
            planetPanel.transform.Find("RightAscension").GetComponent<TextMeshProUGUI>().text = "Right Ascension: " + rightAsc;
            planetPanel.transform.Find("Declination").GetComponent<TextMeshProUGUI>().text = "Declination: " + declin;
            #endregion
            # region Planet GameObject

            GameObject planetGO = Instantiate(Resources.Load(name) as GameObject);
            planetGO.AddComponent<PlanetGO>();
            planetGO.name = name;
            planetGO.transform.localPosition = new Vector3(planet.declination.degrees, planet.declination.minOfArc, planet.declination.secOfArc);
            planetGO.transform.SetParent(GameObject.Find("PlanetGOs").transform, false);

            #endregion

            planetPanel.SetActive(false);

            planetGO.GetComponent<PlanetGO>().myPlanetPanel = planetPanel.GetComponent<Planet>();

            planetPanels.Add(planet);
            planets.Add(planetGO);
        }
    }

    public IEnumerator GetTelescopeSpecs()
    {
        string uri = urlHeader + "GetTelescopeSpecs.php";
        WWWForm form = new WWWForm();

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
                    string jsonArray = webRequest.downloadHandler.text;
                    splitTelescopeInfo(jsonArray);
                    break;
            }
        }
    }
    public void splitTelescopeInfo(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            string mag = jsonArray[i].AsObject["Magnification"] + "x";
            string ap = jsonArray[i].AsObject["Aperture"] + "mm";

            magnifcations.Add(mag);
            apertures.Add(ap);
        }

        magDropDown.ClearOptions();
        apDropDown.ClearOptions();

        magDropDown.AddOptions(magnifcations);
        apDropDown.AddOptions(apertures);
    }
    
}
