using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAddVelocity : MonoBehaviour
{
    public float multiplier = 1;
    private void OnTriggerExit(Collider other)
    {
        
        other.attachedRigidbody.velocity *= multiplier;
    }

}
