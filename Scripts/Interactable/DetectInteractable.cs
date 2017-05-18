using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectInteractable : MonoBehaviour {

    public GameObject interactText;

    void Start () {

        //Spawn Interact Text, set parent as canvas
        //Position it to center of canvas
        interactText = Instantiate(interactText, GameObject.Find("Canvas").transform);
        interactText.transform.localPosition = new Vector3(0, 0);

        interactText.SetActive(false);
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
        interactText.SetActive(false);
        Physics.Raycast(transform.position, transform.forward, out hit, 3);
        if (hit.collider != null && hit.collider.tag == "Interactable")
        {
            //Debug.Log("There is something interactable in front of the object!");
            if (!interactText.activeSelf) interactText.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                hit.collider.SendMessage("Interact", transform.parent.gameObject);
            }
        }
    }
}
