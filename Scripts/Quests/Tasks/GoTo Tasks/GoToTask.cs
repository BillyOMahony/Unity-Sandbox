using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTask : Task {

    //this gameobject should have a collider, player should enter collider
    public GameObject goToObject;
    public bool objectToBeSpawned;
    public Vector3 objectPosition;

    void Start()
    {
        //SpawnObjects();
    }

    public void PlayerArrived()
    {
        quest.CompleteTask(taskNum);
    }

    public override void SpawnObjects()
    {
        Debug.Log("Spawning");
        if (objectToBeSpawned)
        {
            goToObject = Instantiate(goToObject, objectPosition, new Quaternion(0, 0, 0, 0));
            goToObject.GetComponent<DetectPlayer>().task = this;
        }
    }
}
