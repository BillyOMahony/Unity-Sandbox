using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

    public string QuestName;
    public bool QuestComplete;

    public List<Task> TaskList;
    public bool TasksInOrder;

    public Task ActiveTask;

    GameObject _questNotificationText;

    public void CompleteTask()
    {

    }

    void SetQuestOnTasks()
    {
        //Foreach task: task.TaskQuest = this Quest.
    }

}
