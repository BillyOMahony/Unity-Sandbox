using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    public GoToTask task;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")NotifyTask();
    }

    /// <summary>
    /// Notifies task that Player has arrived
    /// </summary>
    void NotifyTask()
    {
        task.PlayerArrived();
    }

}
