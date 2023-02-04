using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SmallRandomStartPosition : MonoBehaviour
{
    public float randomPower;
    
    public void Awake()
    {
        float randX = Random.Range(-randomPower,randomPower);
        float randY = Random.Range(-randomPower,randomPower);
        float randZ = Random.Range(-randomPower,randomPower);
        // speed is a float variable to get the desired speed
        transform.position += new Vector3(randX, randY,randZ);
        
        
    }
}
