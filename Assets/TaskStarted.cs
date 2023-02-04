using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskStarted : MonoBehaviour
{
    public Task TaskToStart;
    // Start is called before the first frame update
    void Start()
    {
        TaskToStart.StartTask();
    }
}
