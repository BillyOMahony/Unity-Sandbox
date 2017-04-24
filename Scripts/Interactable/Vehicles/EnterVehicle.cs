using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehicle : Interactable {

    GameObject _player;
    GameObject _camera;
    GameObject _interactText;
    Vehicle _vehicle;

    void Start()
    {
        _player = GameObject.Find("Player");
        _camera = transform.GetChild(0).gameObject;
        _interactText = GameObject.Find("Interact Text");
        _vehicle = GetComponent<Vehicle>();
    }

	public override void Interact()
    {
        _camera.SetActive(true);
        _player.SetActive(false);
        _interactText.SetActive(false);
        _vehicle.Enter();
    }
}
