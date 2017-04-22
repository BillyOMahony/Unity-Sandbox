using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectInteractable : MonoBehaviour {

    GameObject _interactText;

    void Start () {
        _interactText = GameObject.Find("Interact Text");
        _interactText.SetActive(false);
	}
	
	void FixedUpdate () {
        CheckForInteractable();
    }

    /// <summary>
    /// Raycasts 3 units in front of object.
    /// If hit object has interactable tag, interaction possible. 
    /// </summary>
    void CheckForInteractable()
    {
        RaycastHit hit;
        _interactText.SetActive(false);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
        {
            Debug.Log("There is something in front of the object!");
            if (hit.collider.tag == "Interactable")
            {
                _interactText.SetActive(true);
                if (Input.GetButton("Interact"))
                {
                    hit.collider.SendMessage("Interact");
                }
            }
        }
    }
}
