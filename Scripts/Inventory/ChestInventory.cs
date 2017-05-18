using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestInventory : Interactable
{
    public GameObject itemPanel;
    public List<StoredItem> inventory = new List<StoredItem>();

    GameObject _chestInventoryPanel;
    Transform _canvas;

    void Start()
    {
        _canvas = GameObject.Find("Canvas").transform;
        _chestInventoryPanel = _canvas.Find("ChestInventory Panel").gameObject;
        if (_chestInventoryPanel == null)
        {
            Debug.LogError("Cannot find ChestInventory Panel");
        }

        foreach (StoredItem child in inventory)
        {
            if (child.item.GetComponent<Item>() == null)
            {
                Debug.LogError("Child without Item");
            }
        }
    }

    public override void Interact(GameObject player)
    {
        _chestInventoryPanel.SetActive(true);
        
        // Set height of chestInventoryPanel
        float height = 0;
        height = (80 * inventory.Count) + 80;
        _chestInventoryPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(500, height);

        // Populate Inventory panel
        Transform itemsPanel = _chestInventoryPanel.transform.GetChild(0);

        foreach(Transform child in itemsPanel)
        {
            Destroy(child.gameObject);
        }

        float offset = 0;

        foreach(StoredItem i in inventory)
        {
            Item item = i.item.GetComponent<Item>();

            Transform panel = Instantiate(itemPanel, itemsPanel).transform;
            panel.localPosition = new Vector2(0, offset);
            panel.localScale = new Vector2(1, 1);

            Item.Rarity rarity = item.rarity;

            Image bg = panel.GetChild(0).GetComponent<Image>();
            Image fg = panel.GetChild(0).GetChild(0).GetComponent<Image>();
            Image desc = panel.GetChild(1).GetComponent<Image>();
            Image amt = panel.GetChild(2).GetComponent<Image>();

            RarityScheme scheme;

            switch (rarity){
                case Item.Rarity.Common:
                    scheme = ColorScheme.Instance.common;
                    break;

                case Item.Rarity.Uncommon:
                    scheme = ColorScheme.Instance.uncommon;
                    break;

                case Item.Rarity.Rare:
                    scheme = ColorScheme.Instance.rare;
                    break;

                default:
                    scheme = new RarityScheme(new Color(0,0,0), new Color(0,0,0));
                    Debug.LogError("Cannot Find ColorScheme");
                    break;               
            }

            //Set Colours and Icon
            bg.color = scheme.Background;
            fg.color = scheme.Foreground;
            desc.color = scheme.Background;
            amt.color = scheme.Background;

            fg.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;


            //Set Text
            desc.transform.FindChild("Name").GetComponent<Text>().text = item.name;
            desc.transform.FindChild("Type").GetComponent<Text>().text = rarity.ToString();
            desc.transform.FindChild("Value").GetComponent<Text>().text = "$" + item.value;

            amt.transform.GetChild(0).GetComponent<Text>().text = i.amount.ToString();

            // deal with offset
            offset -= 80;
        }

    }
}

[System.Serializable]
public struct StoredItem
{
    public GameObject item;
    public int amount;
}
