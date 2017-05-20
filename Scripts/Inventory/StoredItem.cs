using UnityEngine;

[System.Serializable]
public struct StoredItem
{
    public Item item;
    public int amount;
}

[System.Serializable]
public struct ChestItem
{
    public GameObject item;
    public int amount;
}
