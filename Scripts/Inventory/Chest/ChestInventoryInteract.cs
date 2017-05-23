using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ChestInventoryInteract : Interactable
{
    public GameObject ItemPanel;

    private GameObject _chestInventoryPanel;
    private Transform _canvas;
    private ChestInventory _chestInv;
    private Inventory _playerInv;
    private GameObject _player;

    void Start()
    {
        _chestInv = GetComponent<ChestInventory>();

        _canvas = GUIPanels.Instance.Canvas.transform;
        _chestInventoryPanel = GUIPanels.Instance.ChestInventoryPanel;
        if (_chestInventoryPanel == null)
        {
            Debug.LogError("Cannot find 'ChestInventory Panel'");
        }
    }

    public override void Interact(GameObject player)
    {
        _player = player;
        SetUpChestInventoryPanel(player);
        //PAUSE PLAYER MOVEMENT
        Time.timeScale = 0f;
        CursorStates.Instance.UnlockCursor();

        //Set Mouse Sensitivity to 0: stops movement of mouse
        //Eventually replace this once I have my own FP controller. This is temporary to work
        //with Unity FPController
        _player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 0f;
        _player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 0f;

    }

    public void SetUpChestInventoryPanel(GameObject player)
    {
        _playerInv = player.GetComponent<Inventory>();

        List<StoredItem> chestInventory = _chestInv.inventory;
        _chestInventoryPanel.SetActive(true);

        // Set height of chestInventoryPanel
        float height = 0;
        height = (80 * chestInventory.Count) + 80;
        _chestInventoryPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(500, height);

        // Remove existing children of chestInventoryPanel
        Transform itemsPanel = _chestInventoryPanel.transform.GetChild(0);
        foreach (Transform child in itemsPanel)
        {
            Destroy(child.gameObject);
        }

        float offset = 0;

        foreach (StoredItem i in chestInventory)
        {
            CreateItemPanel(i, itemsPanel, offset);
            offset -= 80;
        }

        SetUpButtons(player);
    }

    void CreateItemPanel(StoredItem i, Transform itemsPanel, float offset)
    {
        Item item = i.item;

        Transform panel = Instantiate(ItemPanel, itemsPanel).transform;
        panel.localPosition = new Vector2(0, offset);
        panel.localScale = new Vector2(1, 1);
        
        //Get ColorScheme
        Item.Rarity rarity = item.rarity;
        RarityScheme scheme = ColorScheme.Instance.GetRarityScheme(rarity);

        //Get Panels
        Image bg = panel.GetChild(0).GetComponent<Image>();
        Image fg = panel.GetChild(0).GetChild(0).GetComponent<Image>();
        Image desc = panel.GetChild(1).GetComponent<Image>();
        Image amt = panel.GetChild(2).GetComponent<Image>();

        //Set Panel Colour
        bg.color = scheme.Background;
        fg.color = scheme.Foreground;
        desc.color = scheme.Background;
        amt.color = scheme.Background;

        //Set Icon
        fg.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;

        //Set Text
        desc.transform.FindChild("Name").GetComponent<Text>().text = item.name;
        desc.transform.FindChild("Type").GetComponent<Text>().text = rarity.ToString();
        desc.transform.FindChild("Value").GetComponent<Text>().text = "$" + item.value;
        amt.transform.GetChild(0).GetComponent<Text>().text = i.amount.ToString();

        //Set variables
        ChestItemButton cib = panel.GetComponent<ChestItemButton>();
        cib.Player = _player;
        cib.ChestInventory = _chestInv;
        cib.item = i;
    }

    void SetUpButtons(GameObject player)
    {
        ChestButtons takeAll = _chestInventoryPanel.transform.GetChild(1).GetChild(0).GetComponent<ChestButtons>();
        takeAll.Player = player;
        takeAll.ChestInventory = _chestInv;
        
        ChestButtons close = _chestInventoryPanel.transform.GetChild(1).GetChild(1).GetComponent<ChestButtons>();
        close.Player = player;
    }

}
