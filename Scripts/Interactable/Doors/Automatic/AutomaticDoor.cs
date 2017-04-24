using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : AutomaticInteractable {

    public bool doorOpen;
    public float movement;
    public float movementTime;

    float timePassed = 0;

    Vector3 _closedPosition;
    Vector3 _openPosition;

    public bool _inProgress = false;

    void Start()
    {
        _closedPosition = transform.position;
        _openPosition = _closedPosition + Vector3.right * movement;
        doorOpen = false;
    }

    void Update()
    {
        MoveDoor();
    }

    /// <summary>
    /// Opens the door, if the door is not already open.
    /// if door is in process of closing, waits for door to close then
    /// opens door. 
    /// </summary>
    public override void Enter()
    {
        _inProgress = true;
        doorOpen = true;
    }

    /// <summary>
    /// Closes the door, if the door is not already closed.
    /// if door is in process of opening, waits for door to open then
    /// closes door. 
    /// </summary>
    public override void Exit()
    {
        _inProgress = true;
        doorOpen = false;
    }

    void MoveDoor()
    {
        if (_inProgress)
        {
            float fracJourney = timePassed / movementTime;

            if (doorOpen)
            {
                timePassed += Time.deltaTime;
            }
            else
            {
                timePassed -= Time.deltaTime;
            }

            if (timePassed > movementTime)
            {
                _inProgress = false;
                timePassed = movementTime;
                fracJourney = 1;
            }else if(timePassed < 0)
            {
                _inProgress = false;
                timePassed = 0;
                fracJourney = 0;
            }

            transform.position = Vector3.Lerp(_closedPosition, _openPosition, fracJourney);
        }
    }
}
