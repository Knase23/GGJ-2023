using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : MonoBehaviour
{
    private PlayerMovement _movement;
    private Rigidbody _rigidbody;
    private PlayerRooting _rooting;
    public InputActionReference jumpAction;
    public InputActionReference jumpDirectionAction;

    public Animator Animator;
    // Start is called before the first frame update
    public float HorizontalPower = 1.5f;
    public float VerticalPower = 1;

    [SerializeField] private List<Transform> _isGroundedRaycastOrigins = new List<Transform>();

    public bool IsGrounded = true;
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody>();
        _rooting = GetComponent<PlayerRooting>();
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
            if (_rigidbody.velocity.x >= 0)
            {
                Player.Instance.Rotator.TriggerFlip(true, false);
            }
            else
            {
                Player.Instance.Rotator.TriggerFlip(false, false);
            }

        }
    }
    private void Update()
    {
        CheckForGround();
        Animator.SetBool("IsGrounded",IsGrounded);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGrounded)
        {
            Player.Instance.Rotator.SnapToRotation(0f);
        }
        //_movement.enabled = true;
        //_rigidbody.useGravity = false;
    }

    private void CheckForGround()
    {
        if (_rooting.IsRooted)
        {
            IsGrounded = false;
            return;
        }
        bool isGrounded = false;
        foreach (var origin in _isGroundedRaycastOrigins)
        {
            if (Physics.Raycast(origin.transform.position, Vector3.down, 0.75f))
            {
                isGrounded = true;
            }
        }
        IsGrounded = isGrounded;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position , Vector3.up);
    }
}
