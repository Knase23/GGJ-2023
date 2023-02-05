using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public Task nextTask;

    protected virtual void OnTaskComplete()
    {
        if(nextTask && nextTask != this)
            nextTask.StartTask();
            
    }

    public abstract void StartTask();

    public virtual void EndTask()
    {
        OnTaskComplete();
    }
}
