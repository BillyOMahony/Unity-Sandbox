﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemButton : MonoBehaviour
{

    public GameObject Player;
    public ChestInventory ChestInventory;
    public StoredItem item;

    /// <summary>
    /// Adds item to players inventory and removes it from ChestInventory
    /// </summary>
    public void AddToPlayer()
    {
        AddItem();
        ChestInventory.gameObject.GetComponent<ChestInventoryInteract>().SetUpChestInventoryPanel(Player);
    }

    public void AddItem()
    {
        ChestInventory.RemoveItems(item);
        Player.GetComponent<Inventory>().AddItem(item);
    }

}
