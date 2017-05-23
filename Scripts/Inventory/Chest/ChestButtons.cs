using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ChestButtons : MonoBehaviour
{
    private Transform Items;
    public GameObject Player;
    public ChestInventory ChestInventory;

    public void TakeAll()
    {
        Items = GUIPanels.Instance.ChestInventoryPanel.transform.GetChild(0);
        foreach (Transform child in Items)
        {
            child.GetComponent<ChestItemButton>().AddItem();
        }

        ChestInventory.gameObject.GetComponent<ChestInventoryInteract>().SetUpChestInventoryPanel(Player);
    }

    public void Close()
    {
        GUIPanels.Instance.ChestInventoryPanel.SetActive(false);
        Time.timeScale = 1f;

        //Eventually replace
        Player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 2f;
        Player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 2f;
    }

}
