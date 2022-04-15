using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGO : MonoBehaviour
{
    public Planet myPlanetPanel;
    private void OnMouseEnter()
    {
        myPlanetPanel.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        myPlanetPanel.gameObject.SetActive(false);
    }
}
