using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnlyJumpOff : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        playerMovement.MovementEnabled = false;
        playerMovement.GravityEnabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        playerMovement.GravityEnabled = true;
        playerMovement.MovementEnabled = true;
    }
}
