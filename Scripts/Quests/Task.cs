using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour {

    public string TaskName;
    public Quest TaskQuest;
    public string TaskMessage;

    public abstract void CheckTaskStatus();


}
