using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

    public Sprite crosshair;
    private GameObject _crosshairPanel;

    void Start()
    {
        _crosshairPanel = GUIPanels.Instance.Crosshair;
        DisplayCrosshair();
    }

    public void DisplayCrosshair()
    {
        _crosshairPanel.GetComponent<Image>().sprite = crosshair;
    }

}
