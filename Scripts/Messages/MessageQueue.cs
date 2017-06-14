using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageQueue : MonoBehaviour{

    public Text MessageBox;

    // Time in seconds message is displayed for
    public const float MessageAliveTime = 3f;
    // Number of messages on screen at once
    public const int NumMessagesAtOnce = 3;
    // The queue
	private List<Message> _queue = new List<Message>();

    void Update(){
        DisplayMessages();
    }

    /// <summary>
    /// Adds new Item to Queue
    /// </summary>
    /// <param name="message"></param>
    public void AddToQueue(string message){
        Message msg = new Message(message, MessageAliveTime);
        _queue.Add(msg);
    }

    /// <summary>
    /// Displays Messages in MessageBox
    /// </summary>
    void DisplayMessages(){
        string msg = "";
        if(_queue.Count > 0) { 
            for (int i = 0; i < _queue.Count && i < NumMessagesAtOnce; i++){
                
                float time = _queue[i].timer;
                _queue[i].SetTime(time -= Time.deltaTime);
                if (time <= 0){
                    _queue.RemoveAt(i);
                    i--;
                }
                else{
                    msg += _queue[i].GetMessage() + "\n";
                }
            }
        }
        MessageBox.text = msg;
    }


}

public class Message{
    public string message;
    public float timer;

    public Message(string msg, float time){
        message = msg;
        timer = time;
    }

    public string GetMessage(){
        return message;
    }

    public float GetTime(){
        return timer;
    }

    public void SetTime(float time){
        timer = time;
    }

}
