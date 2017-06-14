using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Menu : MonoBehaviour
{
    //Dirtyish solution to mouse look while in inventory problem
    private GameObject _player;

    void Start()
    {

    }

    /// <summary>
    /// Closes Menu Panel and resumes game
    /// </summary>
    public void ClickResume()
    {
        GUIPanels.Instance.MainMenu.SetActive(false);
        CursorStates.Instance.LockCursor();
        Time.timeScale = 1f;


        //Part of dirtyish solution
        _player = GameObject.Find("Player");
        _player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 2f;
        _player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 2f;
    }
    
    /// <summary>
    /// Opens Inventory Panel
    /// </summary>
    public void ClickInventory()
    {
        GUIPanels.Instance.MainMenu.SetActive(false);
        GUIPanels.Instance.Inventory.SetActive(true);

        GUIPanels.Instance.InventoryButtons.GetComponent<InventoryButtons>().LoadInventory();
    }

    /// <summary>
    /// Exits the Game
    /// </summary>
    public void ClickExit(){
        Application.Quit();
    }
}
