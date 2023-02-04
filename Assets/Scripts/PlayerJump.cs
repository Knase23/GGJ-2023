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
    public float HorizontalPower = 1.5f;
    public float VerticalPower = 1;

    public bool IsGrounded = true;
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody>();
        jumpAction.ToInputAction().performed += OnPerformed;
    }

    private void OnPerformed(InputAction.CallbackContext obj)
    {
        //_rigidbody.useGravity = true;
        //_movement.enabled = false;
        //float inputDirection = jumpDirectionAction.ToInputAction().ReadValue<float>(); //Save if we go 2D movement;
        if (IsGrounded)
        {
            _rigidbody.velocity = ((_rigidbody.velocity * HorizontalPower) + Vector3.up * VerticalPower) ;
            IsGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGrounded = true;
        //_movement.enabled = true;
        //_rigidbody.useGravity = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position , direction);
    }
}
