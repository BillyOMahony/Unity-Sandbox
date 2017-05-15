using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCategory : MonoBehaviour {

    List<Item> SubInventory = new List<Item>();

    public void Add(Item item)
    {
        SubInventory.Add(item);
    }
}
