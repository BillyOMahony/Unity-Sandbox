using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : Interactable {
    public GameObject InteractText;

    public abstract void Exit();

    void Awake()
    {
        if (InteractText == null)
        {
            Debug.LogError("InteractText not set up on " + gameObject.name); 
        }
    }
}
