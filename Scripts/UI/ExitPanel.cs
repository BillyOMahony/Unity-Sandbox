using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ExitPanel : MonoBehaviour
{

    private GameObject _player;

    private GameObject _mainMenu;
    private GameObject _chestInventory;
    private GameObject _inventory;

    void Start()
    {
        _mainMenu = GUIPanels.Instance.MainMenu;
        _chestInventory = GUIPanels.Instance.ChestInventoryPanel;
        _inventory = GUIPanels.Instance.Inventory;


        //Eventually replace this
        _player = GameObject.Find("Player");
    }


	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (_chestInventory.activeSelf)
            {
                _chestInventory.SetActive(false);
                Time.timeScale = 1f;
                CursorStates.Instance.LockCursor();

                //Temp
                ChangeMouseLookState(true);

            }
            else if (_mainMenu.activeSelf)
            {
                _mainMenu.SetActive(false);
                Time.timeScale = 1f;
                CursorStates.Instance.LockCursor();

                //Temp
                ChangeMouseLookState(true);
            }
            else if (_inventory.activeSelf)
            {
                _mainMenu.SetActive(true);
                _inventory.SetActive(false);
            }
            else
            {
                _mainMenu.SetActive(true);
                Time.timeScale = 0f;
                CursorStates.Instance.UnlockCursor();

                //Temp
                ChangeMouseLookState(false);
            }
        }
	}


    void ChangeMouseLookState(bool state)
    {
        if (state)
        {
            _player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 2f;
            _player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 2f;
        }
        else
        {
            _player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 0f;
            _player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 0f;
        }
    }
}
