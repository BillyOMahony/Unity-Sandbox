using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<StoredItem> inventory = new List<StoredItem>();
    private MessageQueue Queue;
    public bool IsPlayerInventory = false;

    void Start(){
        Queue = GameObject.Find("InventoryMessageQueue").GetComponent<MessageQueue>();
    }

    /// <summary>
    /// Adds item to inventory, or adds amount if item already in inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(StoredItem item)
    {

        int x = HasItem(item);

        if (x < 0)
        {
            inventory.Add(item);
        }
        else
        {
            StoredItem invItem = inventory[x];
            invItem.amount += item.amount;
            inventory[x] = invItem;
        }

        //Success Message
        if(IsPlayerInventory)Queue.AddToQueue("Item Added: " + item.item.ToString() + " x" + item.amount);

    }

    /*
        This method uses a StoredItem, when removing an item a new StoredItem will have to be
        created where StoredItem.Item is the item to remove and StoredItem.amount is the number to remove
    */
    /// <summary>
    /// Removes amount of item from inventory, if item is in inventory
    /// and amount in inventory > amount
    /// </summary>
    /// <param name="item">item to remove some of</param>
    /// <param name="amount">amount to remove</param>
    public void RemoveItems(StoredItem item)
    {
        int i = HasItem(item);
        if (i >= 0)
        {
            StoredItem invItem = inventory[i];
            if (invItem.amount == item.amount)
            {
                inventory.RemoveAt(i);
            }
            else if (invItem.amount > item.amount)
            {
                invItem.amount -= item.amount;
                inventory[i] = invItem;
            }
            else
            {
                //Some sort of message saying can't remove this many
                //Do nothing
            }
        }

    }

    /// <summary>
    /// Checks if inventory contains item and returns i
    /// </summary>
    /// <param name="item">item to check for</param>
    /// <returns>i is position of item in inventory List</returns>
    private int HasItem(StoredItem item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            StoredItem invItem = inventory[i];

            if (invItem.item.item_Name == item.item.item_Name)
            {
                return i;
            }
        }

        return -1;//Ideally null
    }
}
