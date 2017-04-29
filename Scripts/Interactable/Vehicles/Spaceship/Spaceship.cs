using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : Vehicle {

    GameObject _player;

    public GameObject _camera;
    Transform _cameraPositionPlayer;
    Transform _cameraPositionVechicle;
    public Vector3 _cameraOffset;

    Transform _cannopy;
    Quaternion _cannopyOpenRotation;
    public Vector3 _cannopyClosedRotation;

    SpaceshipController _sc;
    Rigidbody _rb;
    Transform _exitPoint;
    bool _active = false;

    //Animation Variables
    bool _animationInProgress = false;
    public float animTime;
    float _animTimePassed;
    bool _cannopyAnimInProgress;
    public float _cannopyAnimTime;

    // Initilises variables and ensures some aren't null
    void Start () {

        _sc = GetComponent<SpaceshipController>();
        _rb = GetComponent<Rigidbody>();
        _cameraPositionVechicle = transform.Find("CameraHolder");
        if(_cameraPositionVechicle == null)
        {
            Debug.LogError("CameraPositionVehicle: Null");
        }
        _exitPoint = transform.Find("ExitPoint");
        if (_exitPoint == null)
        {
            Debug.LogError("ExitPoint: Null");
        }
        _cannopy = transform.Find("Cannopy");
        if(_cannopy == null)
        {
            Debug.LogError("Cannopy: Null");
        }
        _cannopyOpenRotation = _cannopy.rotation;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_active)Exit();
	}

    /// <summary>
    /// Allows player to enter spaceship
    /// </summary>
    public override void Interact(GameObject player)
    {
        _player = player;
        _camera = _player.transform.Find("Camera").gameObject;
        _cameraOffset = _camera.transform.localPosition;
        _cameraPositionPlayer = _player.transform.Find("CameraHolder");
        //_player.SetActive(false);
        //InteractText.SetActive(false);
        //_sc.Enabled();
        //_active = true;

        //Detact Camera from player and deactivate player
        DetachCamera();
        _player.SetActive(false);

        //Start Animation
        EntryAnimation();

        //Set RB variables
        _rb.useGravity = false;
        _rb.isKinematic = false;
    }

    /// <summary>
    /// Allows player to exit spaceship
    /// </summary>
    public override void Exit()
    {
        if (Input.GetAxis("Interact") < 0)
        {
            //_player.SetActive(true);
            //_player.transform.position = _exitPoint.position;
            _sc.Disabled();
            _rb.useGravity = true;

            ExitAnimation();
        }
    }

    public override void EntryAnimation()
    {
        _animTimePassed = 0;
        _animationInProgress = true;
        StartCoroutine("MoveCameraIn");
    }

    public override void ExitAnimation()
    {
        _active = false;
        _sc.Disabled();
        _animTimePassed = 0;
        _cannopyAnimInProgress = true;
        StartCoroutine(RotateCannopy(Quaternion.Euler(_cannopyClosedRotation), _cannopyOpenRotation, true));
    }

    /// <summary>
    /// Detaches Camera from current parent
    /// </summary>
    void DetachCamera()
    {
        _camera.transform.parent = null;
    }

    /// <summary>
    /// Attaches Camera to newParent
    /// </summary>
    /// <param name="newParent">Transform to attach Camera to</param>
    void AttachCamera(Transform newParent)
    {
        _camera.transform.parent = newParent;
    }

    /// <summary>
    /// Slerps Camera into the Cockpit
    /// Uses position/rotation of camera when player Interacts
    /// and position/rotation of CameraHolder child of this Transform
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveCameraIn()
    {
        while (_animationInProgress)
        {
            _animTimePassed += Time.deltaTime;
            float fracJourney = _animTimePassed / animTime;

            _camera.transform.position = Vector3.Slerp(_cameraPositionPlayer.position, _cameraPositionVechicle.position, fracJourney);
            _camera.transform.rotation = Quaternion.Slerp(_cameraPositionPlayer.rotation, _cameraPositionVechicle.rotation, fracJourney);

            if(fracJourney > 1)
            {
                _animTimePassed = 0;
                _animationInProgress = false;
                _cannopyAnimInProgress = true;

                AttachCamera(transform);

                StartCoroutine(RotateCannopy(_cannopyOpenRotation, Quaternion.Euler(_cannopyClosedRotation), false));
            }

            yield return null;
        }
    }

    /// <summary>
    /// Moves camera out of cockpit
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveCameraOut()
    {
        while (_animationInProgress)
        {
            _animTimePassed += Time.deltaTime;
            float fracJourney = _animTimePassed / animTime;

            _camera.transform.position = Vector3.Slerp(_cameraPositionVechicle.position, _exitPoint.position + _cameraOffset, fracJourney);
            _camera.transform.rotation = Quaternion.Slerp(_cameraPositionVechicle.rotation, _exitPoint.rotation, fracJourney);

            if (fracJourney > 1)
            {
                _animTimePassed = 0;
                _animationInProgress = false;

                DetachCamera();
                AttachCamera(_player.transform);
                _player.SetActive(true);
                _camera.transform.rotation = Quaternion.Euler(0,0,0);
                _player.transform.position = _exitPoint.position;
                _player.transform.rotation = _exitPoint.rotation;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Opens or closes the Cannopy using Slerp
    /// </summary>
    /// <param name="_pos1">Start Position of Slerp</param>
    /// <param name="_pos2">End Position of Slerp</param>
    /// <returns></returns>
    IEnumerator RotateCannopy(Quaternion _pos1, Quaternion _pos2, bool opening)
    {
        while (_cannopyAnimInProgress)
        {
            _animTimePassed += Time.deltaTime;
            float fracJourney = _animTimePassed / _cannopyAnimTime;

            _cannopy.rotation = Quaternion.Slerp(_pos1, _pos2, fracJourney);

            if(fracJourney > 1)
            {
                _cannopyAnimInProgress = false;
                _animationInProgress = true;
                _animTimePassed = 0;
                if (!opening)
                {
                    _sc.Enabled();
                    _active = true;
                }
                else if (opening)
                {
                    StartCoroutine("MoveCameraOut");
                }
            }

            yield return null;
        }
    }
}
