using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGO : MonoBehaviour
{
    public Star myStarPanel;
    private void OnMouseEnter()
    {
        myStarPanel.gameObject.SetActive(true);  
    }
    private void OnMouseExit()
    {
        myStarPanel.gameObject.SetActive(false);  
    }
}
