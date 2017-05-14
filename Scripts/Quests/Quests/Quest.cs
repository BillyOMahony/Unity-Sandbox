using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

    public string questName;

    public GameObject[] taskList;
    public bool[] taskStatus;
    public bool questComplete;

    int _numTasks;

    void Start()
    {
        _numTasks = taskList.Length;
        SetUpTasks();
        SetTaskStatus();
    }


    /// <summary>
    /// Marks Task as complete
    /// </summary>
    /// <param name="num"></param>
    public void CompleteTask(int num)
    {
        //TODO UI notification

        taskStatus[num] = true;

        CheckTasksStatus();
    }

    /// <summary>
    /// Let's each Task Object know the Quest Object.
    /// Spawns Task objects if required
    /// </summary>
    void SetUpTasks()
    {
        int i = 0;
        foreach(GameObject t in taskList)
        {
            Task task = t.GetComponent<Task>();
            Instantiate(task, transform);
            task.quest = this;
            task.SpawnObjects();
            task.taskNum = i;

            i++;
        }
    }

    /// <summary>
    /// Populates taskStatus and sets each bool to false
    /// </summary>
    void SetTaskStatus()
    {
        taskStatus = new bool[taskList.Length];
        for(int x = 0; x < _numTasks - 1; x++)
        {
            taskStatus[x] = false;
        }
    }

    void CheckTasksStatus()
    {
        bool allComplete = true;
        foreach (bool b in taskStatus)
        {
            if (b != true)
            {
                allComplete = false;
                break;
            }
        }
        questComplete = allComplete;

        //TODO if all complete
    }
}
