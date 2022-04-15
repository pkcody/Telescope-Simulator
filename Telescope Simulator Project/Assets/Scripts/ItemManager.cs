using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    Action<string> _createItemsCallback;

    void Start()
    {
        _createItemsCallback = (jsonArrayString) => {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        //StartCoroutine(Main.Instance.Web.GetStarsIDs(_createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;
        print(jsonArrayString);
        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isDone = false;
            string starID = jsonArray[i].AsObject["ID"];
            string name = jsonArray[i].AsObject["Name"];
            //float size = jsonArray[i].AsObject["Size"];
            //float distanceFrom = jsonArray[i].AsObject["DistanceFrom"];
            //string color = jsonArray[i].AsObject["Color"];
            //string rightAsc = jsonArray[i].AsObject["RightAscension"];
            //string declin = jsonArray[i].AsObject["Declination"];
            //string constell = jsonArray[i].AsObject["Constellation"];


            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            //StartCoroutine(Main.Instance.Web.GetStarsIDs(getItemInfoCallback));

            yield return new WaitUntil(() => isDone == true);

            GameObject starGO = Instantiate(Resources.Load("Star") as GameObject);
            Star star = starGO.AddComponent<Star>();
            star.name = name;
            print(star.name);
            //star.size = size;
            //star.distanceFrom = distanceFrom;
            //star.color = color;

            //string[] rA = rightAsc.Split('0');
            //star.rightAscension.hours = float.Parse(rA[0]);
            //star.rightAscension.min = float.Parse(rA[1]);
            //star.rightAscension.sec = float.Parse(rA[2]);
            
            //string[] dec = declin.Split('0');
            //star.declination.degrees = float.Parse(dec[0]);
            //star.declination.minOfArc = float.Parse(dec[1]);
            //star.declination.secOfArc = float.Parse(dec[2]);
       
            //star.constellation = constell;

            //starGO.transform.SetParent(transform.Find("Stars").transform);
            //starGO.transform.localScale = Vector3.one;
            //starGO.transform.localPosition = Vector3.zero;

            starGO.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = itemInfoJson["Name"];
            //starGO.transform.Find("Constellation").GetComponent<TextMeshProUGUI>().text = itemInfoJson["Constellation"];
            //starGO.transform.Find("Size").GetComponent<TextMeshProUGUI>().text = itemInfoJson["Size"];
            //starGO.transform.Find("DistanceFrom").GetComponent<TextMeshProUGUI>().text = itemInfoJson["DistanceFrom"];
            //starGO.transform.Find("Color").GetComponent<TextMeshProUGUI>().text = itemInfoJson["Color"];
            //starGO.transform.Find("RightAscension").GetComponent<TextMeshProUGUI>().text = itemInfoJson["RightAscension"];
            //starGO.transform.Find("Declination").GetComponent<TextMeshProUGUI>().text = itemInfoJson["Declination"];

            //int imgVer = itemInfoJson["imgVer"].AsInt;
            //byte[] bytes = ImageManager.Instance.LoadImage(itemId, imgVer);

            //if (bytes.Length == 0)
            //{
            //    Action<byte[]> getItemIconCallback = (downloadedBytes) =>
            //    {
            //        Sprite sprite = ImageManager.Instance.BytesToSprite(downloadedBytes);
            //        itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            //        ImageManager.Instance.SaveImage(itemId, downloadedBytes, imgVer);
            //        ImageManager.Instance.SaveVersionJson();
            //    };
            //    StartCoroutine(Main.Instance.Web.GetItemIcon(itemId, getItemIconCallback));
            //}

            //else
            //{
            //    Debug.Log("LOADING ICON FROM DEVICE: " + itemId);
            //    Sprite sprite = ImageManager.Instance.BytesToSprite(bytes);
            //    itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            //}

            //itemGo.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            //{
            //    string idInInventory = id;
            //    string iId = itemId;
            //    string userId = Main.Instance.UserInfo.UserID;
            //    StartCoroutine(Main.Instance.Web.SellItem(idInInventory, itemId, userId));
            //});

        }

    }
}