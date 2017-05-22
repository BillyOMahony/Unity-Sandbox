using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    //public Item.Type type;

    public void GoToSubInventory(GameObject obj)
    {
        Item.Type type = obj.GetComponent<SubInvType>().type;
        GameObject.Find("Player").GetComponent<PlayerInventory>().UpdateGUI(type);
    }

}
