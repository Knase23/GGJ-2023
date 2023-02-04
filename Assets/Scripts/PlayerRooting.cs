using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _movement = GetComponent<PlayerMovement>();
        _jump = GetComponent<PlayerJump>();
        _jumpAction.ToInputAction().performed += TriggerLaunch;
    }

    private void TriggerLaunch(InputAction.CallbackContext obj)
    {
        Vector2 direction = _inputDirection.action.ReadValue<Vector2>();
        if (!(direction.x > 0 && _rootNormal.x > 0) || !(direction.x < 0 && _rootNormal.x < 0))
        {

            direction = new Vector2(-direction.x, direction.y);
        }
        if (direction.y > 0.25f)
        {
            if (_rootNormal.x > 0)
            {
                var launchVector = Quaternion.Euler(0, 0, 45f) * _rootNormal;
                Debug.Log(launchVector);
                LaunchFromRooted(launchVector);
            }
            else
            {
                var launchVector = Quaternion.Euler(0, 0, -45f) * _rootNormal;
                Debug.Log(launchVector);
                LaunchFromRooted(launchVector);
            }
        }
        else if (direction.y < -0.25f)
        {
            if (_rootNormal.x > 0)
            {
                var launchVector = Quaternion.Euler(0, 0, -45f) * _rootNormal;
                Debug.Log(launchVector);
                LaunchFromRooted(launchVector);
            }
            else
            {
                var launchVector = Quaternion.Euler(0, 0, 45f) * _rootNormal;
                Debug.Log(launchVector);
                LaunchFromRooted(launchVector);
            }

        }
        else
        {
            Debug.Log(_rootNormal);
            LaunchFromRooted(_rootNormal);
        }
    }

    public void RootToSurface(Vector3 position, Vector2 normal) 
    {
        if (IsRooted) return;
        IsRooted = true;
        _rootNormal = normal;

        transform.position = position;
        _rb.velocity = Vector3.zero;
        _movement.enabled = false;
        _movement.GravityEnabled = false;
        _jump.enabled = false;

        //rotate visuals to normal
    }

    public void LaunchFromRooted(Vector2 direction) 
    {
        IsRooted = false;
        _rb.velocity = direction * _launchPower;
        _movement.enabled = true;
        _movement.GravityEnabled = true;
        _jump.enabled = true;
    }
}
