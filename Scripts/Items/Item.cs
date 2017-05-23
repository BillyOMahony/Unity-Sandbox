using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour {

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare
    };

    public enum Type {
        Weapon,
        Armor, 
        Resource,
        Scrap       
    };

    public string item_Name;
    public string description;
    public GameObject prefab;
    public float weight;
    public Sprite icon;
    public int value;
    public Rarity rarity;
    public Type type;

}
