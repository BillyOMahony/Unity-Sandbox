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
        Physics.Raycast(transform.position, transform.forward, out hit, 3);
        if (hit.collider != null && hit.collider.tag == "Interactable")
        {
            //Debug.Log("There is something interactable in front of the object!");
            if (!_interactText.activeSelf)_interactText.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                hit.collider.SendMessage("Interact");
            }
        }
    }
}
