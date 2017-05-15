using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public InventoryCategory ScrapInventory = new InventoryCategory();

    public Dictionary<int, InventoryCategory> inventory = new Dictionary<int, InventoryCategory>();

    void Start()
    {
        inventory.Add(0, ScrapInventory);
    }

    public void AddItem(Item item)
    {
        Item.Type type = item.type;

        switch (type)
        {
            case Item.Type.Scrap:
                ScrapInventory.Add(item);
                break;
        }
    }
}
