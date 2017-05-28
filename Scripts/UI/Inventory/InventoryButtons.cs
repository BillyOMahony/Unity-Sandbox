using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    public Item.Type InitialInventory;

    /// <summary>
    /// Calls UpdateGUI for relevant Type
    /// </summary>
    /// <param name="obj"></param>
    public void GoToSubInventory(GameObject obj)
    {
        Item.Type type = obj.GetComponent<SubInvType>().type;
        GameObject.Find("Player").GetComponent<PlayerInventory>().UpdateGUI(type);
    }

    /// <summary>
    /// Initial inventory to open when player opens inventory
    /// </summary>
    public void LoadInventory()
    {
        GameObject.Find("Player").GetComponent<PlayerInventory>().UpdateGUI(InitialInventory);
    }
}
