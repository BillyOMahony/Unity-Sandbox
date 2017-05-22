﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPanels : Singleton<GUIPanels>
{
    protected GUIPanels() { }

    public GameObject Canvas;
    public GameObject ChestInventoryPanel;
    public GameObject MainMenu;
    public GameObject Inventory;
    public GameObject PlayerInventoryItemsPanel;

}
