using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : MonoBehaviour
{
    private PlayerMovement _movement;
    private Rigidbody _rigidbody;
    public InputActionReference jumpAction;

    public InputActionReference jumpDirectionAction;
    // Start is called before the first frame update
    public Vector3 direction = Vector3.up;
    public float power = 1;
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody>();
        jumpAction.ToInputAction().performed += OnPerformed;
    }

    private void OnPerformed(InputAction.CallbackContext obj)
    {
        _rigidbody.useGravity = true;
        _movement.enabled = false;
        float inputDirection = jumpDirectionAction.ToInputAction().ReadValue<float>(); //Save if we go 2D movement;
        _rigidbody.velocity = direction * power;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _movement.enabled = true;
        _rigidbody.useGravity = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position , direction);
    }
}