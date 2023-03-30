using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message {
    public static Queue<Message> MessageQueue = new Queue<Message>();
    public static bool isQueuing = false;

    public virtual void AddToQueue() {
        MessageQueue.Enqueue(this);
        if (!isQueuing) {
            // TODO ReadMassageFromMQ();
        }
    }

    public virtual void DealMessage() {

    }

    public static void DoneMessage() {
        if (MessageQueue.Count > 0) {
            // TODO ReadMassageFromMQ();
        }
        else {
            isQueuing = false;
        }
    }
    public static void ReadMessage() {
        isQueuing = true;
        MessageQueue.Dequeue().DealMessage();
    }
}
