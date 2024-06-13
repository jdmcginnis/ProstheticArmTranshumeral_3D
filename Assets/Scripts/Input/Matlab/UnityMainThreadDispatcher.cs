using System;
using System.Collections.Generic;
using UnityEngine;

// We listen for external input on a parallel thread via MatlabConnection.cs
// This allows us to take these inputs and transfer them to the main execution thread that Unity runs on
// These inputs, newly transferred to the main thread, are queued in the actionQueue
// Every 
public class UnityMainThreadDispatcher : MonoBehaviour {
    private static UnityMainThreadDispatcher instance;
    private static readonly Queue<Action> actionQueue = new Queue<Action>();


    public void InitializeMainThreadDispatcher()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // We want to process inputs every fixed frame; keep the timestep constant
    private void FixedUpdate() {
        lock (actionQueue) {
            while (actionQueue.Count > 0) {
                Action action = actionQueue.Dequeue();
                action.Invoke();
            }
        }
    }

    // MatlabConnection.cs puts the action of reading input in the queue
    public static void ExecuteOnMainThread(Action action) {

        lock (actionQueue) {
            actionQueue.Enqueue(action);
        }
    }
}
