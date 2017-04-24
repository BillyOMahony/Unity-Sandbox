using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : Vehicle {

    GameObject _player;
    GameObject _camera;

    SpaceshipController _sc;
    Rigidbody _rb;
    Transform _exitPoint;
    bool _active = false;

	// Use this for initialization
	void Start () {
        _player = GameObject.Find("Player");
        _sc = GetComponent<SpaceshipController>();
        _rb = GetComponent<Rigidbody>();
        _camera = transform.GetChild(0).gameObject;
        _exitPoint = transform.GetChild(1);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_active)Exit();
	}

    /// <summary>
    /// Allows player to enter spaceship
    /// </summary>
    public override void Interact()
    {
        _camera.SetActive(true);
        _player.SetActive(false);
        InteractText.SetActive(false);
        _sc.Enabled();
        _rb.isKinematic = false;
        _rb.useGravity = false;
        _active = true;
    }

    /// <summary>
    /// Allows player to exit spaceship
    /// </summary>
    public override void Exit()
    {
        if (Input.GetAxis("Interact") < 0)
        {
            transform.GetChild(0).gameObject.SetActive(false); // Set camera unactive when player exits
            _player.SetActive(true);
            _player.transform.position = _exitPoint.position;
            _sc.Disabled();
            _active = false;
            _rb.useGravity = true;
        }
    }
}
