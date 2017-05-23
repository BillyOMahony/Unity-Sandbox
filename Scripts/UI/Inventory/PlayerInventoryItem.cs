using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryItem : MonoBehaviour
{
    public StoredItem Item;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /// <summary>
    /// Updates the Item Panel of Player inventory when an item is clicked
    /// </summary>
    public void Clicked()
    {
        GameObject itemPanel = GUIPanels.Instance.InventoryItemPanel;
        itemPanel.transform.GetChild(0).GetComponent<Text>().text = Item.item.name;
        itemPanel.transform.GetChild(1).GetComponent<Text>().text = Item.item.rarity.ToString();
        itemPanel.transform.GetChild(2).GetComponent<Text>().text = Item.item.description;
        itemPanel.transform.GetChild(3).GetComponent<Image>().sprite = Item.item.icon;
        itemPanel.transform.GetChild(3).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
}
