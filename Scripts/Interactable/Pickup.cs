using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable {

    StoredItem item;

	public override void Interact(GameObject player)
    {
        item.item = gameObject.GetComponent<Item>();
        item.amount = 1;
        player.GetComponent<Inventory>().AddItem(item);
        Destroy(gameObject);
    }
}
