using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoubleDoors : AutomaticInteractable {

    /// <summary>
    /// Call Enter() on children
    /// </summary>
    public override void Enter()
    {
        transform.GetChild(0).GetComponent<AutomaticInteractable>().Enter();
        transform.GetChild(1).GetComponent<AutomaticInteractable>().Enter();
    }

    /// <summary>
    /// Call Exit() on children
    /// </summary>
    public override void Exit()
    {
        transform.GetChild(0).GetComponent<AutomaticInteractable>().Exit();
        transform.GetChild(1).GetComponent<AutomaticInteractable>().Exit();
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
        if(other.tag == "Player")
        {
            Exit();
        }
    }
}
