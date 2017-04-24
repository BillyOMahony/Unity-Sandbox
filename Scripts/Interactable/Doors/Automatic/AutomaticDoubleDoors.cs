using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoubleDoors : AutomaticInteractable {

    public override void Enter()
    {
        transform.GetChild(0).GetComponent<AutomaticInteractable>().Enter();
        transform.GetChild(1).GetComponent<AutomaticInteractable>().Enter();
    }

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
