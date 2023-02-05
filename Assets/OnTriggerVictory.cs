using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerVictory : MonoBehaviour
{
    public UnityEvent OnTrigger;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Victory!");
        OnTrigger?.Invoke();
    }
}
