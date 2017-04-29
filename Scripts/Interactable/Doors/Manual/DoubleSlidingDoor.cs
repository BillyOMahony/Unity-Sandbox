using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSlidingDoor : Interactable {

    GameObject leftDoor;
    GameObject rightDoor;
    bool state = false; //false = closed, true = open

	// Use this for initialization
	void Start () {
        leftDoor = transform.GetChild(0).gameObject;
        rightDoor = transform.GetChild(1).gameObject;
	}

    public override void Interact(GameObject player)
    {
        leftDoor.GetComponent<SlidingDoor>().Interact(null);
        rightDoor.GetComponent<SlidingDoor>().Interact(null);
    }
}
