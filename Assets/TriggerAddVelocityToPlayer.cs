using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddVelocityToPlayer : MonoBehaviour
{
    private MovingBox _movingBox;

    private Vector2 recordedVelocity;

    public float multiplier = 1;

    public float gracePeriod;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        _movingBox = GetComponent<MovingBox>();
    }

    private void OnTriggerExit(Collider other)
    {
        if( timer > gracePeriod && Vector2.Dot(_movingBox.velocity, Vector2.down) > 0) return;
        if(timer > gracePeriod  && Vector3.Dot(other.attachedRigidbody.velocity,Vector3.down) > 0.1f) return;
        
        other.attachedRigidbody.velocity += (Vector3)recordedVelocity * multiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector2.Dot(_movingBox.velocity, Vector2.up) > 0)
        {
            recordedVelocity = _movingBox.velocity;
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
