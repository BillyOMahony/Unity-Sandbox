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

    public override void Interact()
    {
        leftDoor.GetComponent<SlidingDoor>().Interact();
        rightDoor.GetComponent<SlidingDoor>().Interact();
    }
}
