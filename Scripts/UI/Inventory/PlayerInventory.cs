﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    public Transform ItemsPanel;
    public GameObject InventoryItem;
    //public Item.Type type;
    public List<StoredItem> inventory;

    // Use this for initialization
    void Start ()
	{
	    ItemsPanel = GUIPanels.Instance.PlayerInventoryItemsPanel.transform;
	    //type = Item.Type.Scrap;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Updates the Inventory with all items of Type type
    /// </summary>
    /// <param name="type"></param>
    public void UpdateGUI(Item.Type type)
    {
        GUIPanels.Instance.SubInventoryText.GetComponent<Text>().text = type.ToString();

        ClearItemPanel();

        //Delete All Existing Items in Inventory
        ClearInvGUI();

        inventory = GetComponent<Inventory>().inventory;

        float xOffset = 0;
        // For some reason items appear at -760, yOffset = 760 offsets this.
        float yOffset = 760;
        int counter = 0;

        foreach (StoredItem item in inventory)
        {
            if (item.item.type == type)
            {
                
                GameObject invItem = Instantiate(InventoryItem, ItemsPanel);
                invItem.transform.localPosition = new Vector2(xOffset, yOffset);
                invItem.transform.localScale = new Vector2(1, 1);
                Debug.Log(invItem.transform.localPosition.ToString());

                RarityScheme rarity = ColorScheme.Instance.GetRarityScheme(item.item.rarity);

                invItem.GetComponent<Image>().color = rarity.Foreground;
                invItem.transform.GetChild(0).GetComponent<Image>().color = rarity.Background;
                invItem.transform.GetChild(1).GetComponent<Image>().sprite = item.item.icon;
                invItem.transform.GetChild(2).GetComponent<Text>().text = item.amount.ToString();

                invItem.GetComponent<PlayerInventoryItem>().Item = item;

                xOffset += 180;
                if (counter > 6)
                {
                    xOffset = 0;
                    yOffset += -180;
                }
            }
        }
    }

    /// <summary>
    /// Removes all children panels of ItemsPanel
    /// </summary>
    void ClearInvGUI()
    {
        foreach (Transform child in ItemsPanel)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Clears the Item Panel
    /// </summary>
    void ClearItemPanel()
    {
        GameObject itemPanel = GUIPanels.Instance.InventoryItemPanel;
        itemPanel.transform.GetChild(0).GetComponent<Text>().text = "";
        itemPanel.transform.GetChild(1).GetComponent<Text>().text = "";
        itemPanel.transform.GetChild(2).GetComponent<Text>().text = "";
        itemPanel.transform.GetChild(3).GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
    }

}
