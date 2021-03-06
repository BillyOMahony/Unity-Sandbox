﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : AutomaticInteractable {

    public enum Direction
    {
        Up,
        Right,
        Forward
    }

    public bool doorOpen;
    public float movement;
    public Direction direction;
    public float movementTime;

    float timePassed = 0;

    Vector3 _closedPosition;
    Vector3 _openPosition;

    public bool _inProgress = false;

    void Start()
    {
        _closedPosition = transform.position;
        doorOpen = false;

        switch (direction)
        {
            case Direction.Right:
                _openPosition = _closedPosition + Vector3.right * movement;
                break;

            case Direction.Up:
                _openPosition = _closedPosition + Vector3.up * movement;
                break;

            case Direction.Forward:
                _openPosition = _closedPosition + Vector3.forward * movement;
                break;
        }

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

    /// <summary>
    /// Lerps the door. This function is constantly called in Update
    /// if _inProgress = true then the door will be lerped this frame
    /// </summary>
    void MoveDoor()
    {
        if (_inProgress)
        {
            float fracJourney = timePassed / movementTime;

            if (doorOpen)timePassed += Time.deltaTime;
            else timePassed -= Time.deltaTime;

            //If timePassed at min or max, don't lerp after this frame. 
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Enter();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Exit();
        }
    }

}
