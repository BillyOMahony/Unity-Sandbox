using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPanel : MonoBehaviour
{
    private GameObject MainMenu;

    void Start()
    {
        MainMenu = GUIPanels.Instance.MainMenu;
    }


	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (MainMenu.activeSelf)
            {
                MainMenu.SetActive(false);
                Time.timeScale = 1f;
                CursorStates.Instance.LockCursor();
            }
            else
            {
                MainMenu.SetActive(true);
                Time.timeScale = 0f;
                CursorStates.Instance.UnlockCursor();
            }
        }
	}
}
