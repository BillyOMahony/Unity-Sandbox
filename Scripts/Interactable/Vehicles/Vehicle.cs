using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : Interactable {
    public GameObject InteractText;

    public abstract void Exit();
    public abstract void EntryAnimation();
    public abstract void ExitAnimation();

    void Awake()
    {
        if (InteractText == null)
        {
            Debug.LogError("InteractText not set up on " + gameObject.name); 
        }
    }
}
