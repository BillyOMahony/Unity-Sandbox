using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestInventory : Inventory
{
    public List<ChestItem> InitialInventory = new List<ChestItem>();

    void Start()
    {
        Checks();
        PopulateStoredList();
    }

    /// <summary>
    /// Checks to run to ensure Inventory of this chest is set up correctly
    /// </summary>
    void Checks()
    {
        foreach (ChestItem ci in InitialInventory)
        {
            if (ci.item.GetComponent<Item>() == null)
            {
                Debug.LogError(ci.item.name + " is not an item, remove from Inventory");
            }
            if (ci.amount < 1)
            {
                Debug.LogError(gameObject.name + " Item in inventory has amount of 0");
            }
        }
    }

    /// <summary>
    /// Takes Item component from each item of ChestItem in inventory and 
    /// adds to inventory list. Also takes amount. 
    /// </summary>
    void PopulateStoredList()
    {
        foreach (ChestItem i in InitialInventory)
        {
            StoredItem item;
            item.item = i.item.GetComponent<Item>();
            item.amount = i.amount;

            inventory.Add(item);
        }
    }
}