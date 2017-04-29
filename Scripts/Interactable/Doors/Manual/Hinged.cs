using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinged : Interactable {

    public Vector3 rotation;
    Quaternion _rotation;
    public float rotationTime;
    public bool doorOpen = false;

    public bool hasHandle;
    public GameObject handle;

    public Vector3 handleRotation;
    Quaternion _handleRotation = Quaternion.Euler(0, 0, -80);
    public float handleRotationTime;

    public Quaternion _openPosition;
    public Quaternion _closedPosition;

    public Quaternion _handleOpenPosition;
    public Quaternion _handleClosedPosition;

    bool _inProgress = false;
    bool _handleInProgress = false;
    bool _handleEnd = false;

    float timePassedHandle;
    float timePassedDoor;

    void Start()
    {
        _rotation = Quaternion.Euler(rotation);
        _closedPosition = transform.localRotation;
        _openPosition = _closedPosition * _rotation;

        if (hasHandle)
        {
            _handleRotation = Quaternion.Euler(handleRotation);
            _handleOpenPosition = handle.transform.rotation;
            _handleClosedPosition = _handleOpenPosition * _handleRotation;
        }
    }

    public override void Interact(GameObject player)
    {
        if (!_inProgress)
        {
            _inProgress = true;
            //Rotate Handle
            if (!doorOpen && hasHandle)
            {
                _handleInProgress = true;
                _handleEnd = false;
                timePassedHandle = 0;
                StartCoroutine(RotateHandle());
            }

            //Rotate the door
            timePassedDoor = 0;
            if (!doorOpen)
            {
                doorOpen = true;
                StartCoroutine(RotateDoor(_closedPosition, _openPosition));
            }
            else
            {
                doorOpen = false;
                StartCoroutine(RotateDoor(_openPosition, _closedPosition));
            }
        }
    }

    /// <summary>
    /// Rotates the door handle
    /// Only rotate if the door is not open
    /// Resets local y angle at end of each frame
    /// </summary>
    /// <returns></returns>
    IEnumerator RotateHandle()
    {
        while (!_handleEnd)
        {
            if (_handleInProgress)
            {
                timePassedHandle += Time.deltaTime;
            }
            else
            {
                timePassedHandle -= Time.deltaTime;
                if (timePassedHandle <= 0)
                {
                    _handleEnd = true;
                }
            }

            float fracJourney = timePassedHandle / handleRotationTime;

            if (fracJourney >= 1)
            {
                _handleInProgress = false;
            }

            handle.transform.rotation = Quaternion.Lerp(_handleOpenPosition, _handleClosedPosition, fracJourney);

            //Reset local angle of handle
            Vector3 angle;
            angle = handle.transform.rotation.eulerAngles;
            angle.y = 90;
            handle.transform.localRotation = Quaternion.Euler(angle);

            yield return null;
        }
    }

    /// <summary>
    /// Rotates the door
    /// </summary>
    /// <param name="fromPos">Position to begin lerp at</param>
    /// <param name="toPos">Position to end lerp at</param>
    /// <returns></returns>
    IEnumerator RotateDoor(Quaternion fromPos, Quaternion toPos)
    {
        while (_inProgress)
        {

            timePassedDoor += Time.deltaTime;

            float fracJourney = timePassedDoor / rotationTime;

            if (fracJourney >= 1)
            {
                _inProgress = false;
            }

            transform.rotation = Quaternion.Lerp(fromPos, toPos, fracJourney);

            yield return null;
        }
    }
}
