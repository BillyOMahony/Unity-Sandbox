using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : Interactable {

    public bool doorOpen;
    public float movement;
    public float movementTime;

    float timePassed = 0;
     
    Vector3 _closedPosition;
    Vector3 _openPosition;

    bool _inProgress = false;

    void Start()
    {
        _closedPosition = transform.localPosition;
        _openPosition = _closedPosition + Vector3.right * movement;
    }

    /// <summary>
    /// Opens or closes the door. 
    /// Only calls the Coroutine if the door is not in the process of opening or closing.
    /// </summary>
    public override void Interact(GameObject player)
    {
        if (!_inProgress)
        {
            timePassed = 0;
            _inProgress = true;
            if (doorOpen)
            {
                doorOpen = false;
                StartCoroutine(MoveDoor(_openPosition, _closedPosition));
            }
            else
            {
                doorOpen = true;
                StartCoroutine(MoveDoor(_closedPosition, _openPosition));
            }
        }
    }

    /// <summary>
    /// Lerps door from startPos to endPos.
    /// Ends after set amount of time.
    /// </summary>
    /// <param name="startPos">Start position of the Lerp</param>
    /// <param name="endPos">End position of the Lerp</param>
    /// <returns></returns>
    IEnumerator MoveDoor(Vector3 startPos, Vector3 endPos)
    {
        while (_inProgress)
        {
            timePassed += Time.deltaTime;
            float fracJourney = timePassed / movementTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, fracJourney);
            if (timePassed > movementTime) {
                _inProgress = false;
            }
            yield return null;
        }
    }
}
