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
        Scrap       
    };

    public string item_Name;
    public float weight;
    public Image icon;
    public int value;
    public Rarity rarity;
    public Type type;

}
