using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRooting : MonoBehaviour
{
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _inputDirection;

    [SerializeField] private float _launchPower = 10f;
    private Rigidbody _rb = null;
    private PlayerMovement _movement = null;
    private PlayerJump _jump = null;

    private Vector2 _rootNormal = Vector2.zero;
    public bool IsRooted = false;

    private Coroutine _movementCoroutine = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _movement = GetComponent<PlayerMovement>();
        _jump = GetComponent<PlayerJump>();
        _jumpAction.ToInputAction().performed += TriggerLaunch;
    }

    private void TriggerLaunch(InputAction.CallbackContext obj)
    {
        if (!IsRooted) return;
        Vector2 direction = _inputDirection.action.ReadValue<Vector2>();
        if (!(direction.x > 0 && _rootNormal.x > 0) || !(direction.x < 0 && _rootNormal.x < 0))
        {
            direction = new Vector2(-direction.x, direction.y);
        }
        if (direction.y > 0)
        {
            if (_rootNormal.x >= 0)
            {
                var launchVector = Quaternion.Euler(0, 0, 60f) * _rootNormal;
                Debug.DrawRay(transform.position, launchVector * 10f, Color.red, 1f);
                LaunchFromRooted(launchVector);
            }
            else
            {
                var launchVector = Quaternion.Euler(0, 0, -60f) * _rootNormal;
                Debug.DrawRay(transform.position, launchVector * 10f, Color.red, 1f);
                LaunchFromRooted(launchVector);
            }
        }
        else if (direction.y < 0)
        {
            if (_rootNormal.x >= 0)
            {
                var launchVector = Quaternion.Euler(0, 0, -45f) * _rootNormal;
                Debug.DrawRay(transform.position, launchVector * 10f, Color.red, 1f);
                LaunchFromRooted(launchVector);
            }
            else
            {
                var launchVector = Quaternion.Euler(0, 0, 45f) * _rootNormal;
                Debug.DrawRay(transform.position, launchVector * 10f, Color.red, 1f);
                LaunchFromRooted(launchVector);
            }

        }
        //else
        //{
        //    Debug.Log(_rootNormal);
        //    LaunchFromRooted(_rootNormal);
        //}
    }

    public void RootToSurface(Vector3 position, Vector2 normal) 
    {
        if (IsRooted) return;
        IsRooted = true;
        _rootNormal = normal;

        transform.position = position;
        _rb.velocity = Vector3.zero;
        _movement.MovementEnabled = false;
        _movement.GravityEnabled = false;
        _jump.enabled = false;

        //rotate visuals to normal
    }

    public void LaunchFromRooted(Vector2 direction) 
    {
        IsRooted = false;
        _rb.velocity = direction * _launchPower;
        _movement.GravityEnabled = true;
        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
        }
        _movementCoroutine = StartCoroutine(ReEnableMovementAfterSeconds(0.3f));
    }

    private IEnumerator ReEnableMovementAfterSeconds(float time) 
    {
        yield return new WaitForSeconds(time);
        _movement.MovementEnabled = true;
        _jump.enabled = true;
    }
}
