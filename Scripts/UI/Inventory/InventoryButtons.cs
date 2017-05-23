using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    public Item.Type InitialInventory;

    public void GoToSubInventory(GameObject obj)
    {
        Item.Type type = obj.GetComponent<SubInvType>().type;
        GameObject.Find("Player").GetComponent<PlayerInventory>().UpdateGUI(type);
    }

    public void LoadInventory()
    {
        GameObject.Find("Player").GetComponent<PlayerInventory>().UpdateGUI(InitialInventory);
    }
}
