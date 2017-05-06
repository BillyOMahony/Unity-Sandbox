using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour {

    public string taskName;
    public Quest quest;
    public int taskNum;

    public abstract void SpawnObjects();

}
