using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    void Start()
    {

    }

    public void ClickResume()
    {
        GUIPanels.Instance.MainMenu.SetActive(false);
        CursorStates.Instance.LockCursor();
    }

    public void ClickInventory()
    {
        GUIPanels.Instance.MainMenu.SetActive(false);
        GUIPanels.Instance.Inventory.SetActive(true);
    }
}
