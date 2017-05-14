using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour {

    public string name;
    public Image icon;

    public void Collect(Transform player)
    {
        Transform inv = player.Find("PlayerInformation/Inventory");
        if (inv == null)
        {
            Debug.LogError("NO INVENTORY");
        }
        else
        {
            Debug.Log("Adding " + name + " to Inventory");
        }
        ///ToDO
        ///Make Inventory and have Add methods
        ///Automatic pickup should be for misc items; ammo and the likes
        ///non-automatic pickup for ingrediants/chest stuff/weapons etc
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Collect(other.transform);
        }
    }
}
