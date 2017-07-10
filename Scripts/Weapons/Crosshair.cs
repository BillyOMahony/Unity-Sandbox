using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

    public Sprite crosshair;
    private GameObject _crosshair;

    void Start()
    {
        _crosshair = GUIPanels.Instance.Crosshair;
        DisplayCrosshair();
    }

    public void DisplayCrosshair()
    {
        _crosshair.GetComponent<Image>().sprite = crosshair;
    }

}
