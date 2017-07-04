using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteInteractable : Interactable {

    public GameObject LinkedInteractable;

    public override void Interact(GameObject player)
    {
        LinkedInteractable.GetComponent<Interactable>().Interact(player);
    }

}
