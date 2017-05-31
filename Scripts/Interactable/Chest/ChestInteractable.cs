using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable {

    public Transform Lid;
    public Vector3 OpenPosition;
    public float OpeningTime = .5f;

    private Quaternion _closedPosition;
    private Quaternion _openPosition;

    private float _fracJourney;
    private bool _isOpening;
    private bool _inProgress;
    private float _timePassed = 0f;

    void Start(){
        _closedPosition = Lid.localRotation;
        _openPosition = Quaternion.Euler(OpenPosition);
    }

    public override void Interact(GameObject player){
        if (_isOpening) Close();
        else Open(player);
    }

    /// <summary>
    /// Opens Lid
    /// </summary>
    /// <param name="player"></param>
    public void Open(GameObject player){
        _isOpening = true;
        if(!_inProgress)StartCoroutine(OpenLid(player));
    }

    /// <summary>
    /// Closes Lid
    /// </summary>
    public void Close(){
        _isOpening = false;
        if(!_inProgress)StartCoroutine(OpenLid(null));
    }

    /// <summary>
    /// Rotates the lid over time
    /// Direction of rotation depends on _isOpening bool
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator OpenLid(GameObject player){

        _inProgress = true;

        while (_inProgress){

            if (_isOpening) _timePassed += Time.deltaTime;
            else _timePassed -= Time.deltaTime;

            _fracJourney = _timePassed / OpeningTime;

            Lid.localRotation = Quaternion.Lerp(_closedPosition, _openPosition, _fracJourney);

            if (_isOpening && _timePassed > OpeningTime){
                _inProgress = false;
                GetComponent<ChestInventoryGUI>().SetUp(player);
            }else if (!_isOpening && _timePassed < 0){
                _inProgress = false;
            }

            yield return null;
        }
    }
}
