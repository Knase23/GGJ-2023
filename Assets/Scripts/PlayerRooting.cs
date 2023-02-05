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


    public Animator Animator;
    [SerializeField] private float _launchPower = 10f;
    [SerializeField] private AudioCue _rootingSFX = null;
    [SerializeField] private AudioCue _launchSFX = null;
    [SerializeField] private float _lockedMovementTime = 0.225f;
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
        if (direction.y >= -0.3f)
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
                var launchVector = Quaternion.Euler(0, 0, -30f) * _rootNormal;
                Debug.DrawRay(transform.position, launchVector * 10f, Color.red, 1f);
                LaunchFromRooted(launchVector);
            }
            else
            {
                var launchVector = Quaternion.Euler(0, 0, -30f) * _rootNormal;
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
        Animator.SetBool("Rooted", true);
        _rootNormal = normal;

        transform.position = position;
        _rb.velocity = Vector3.zero;
        _movement.MovementEnabled = false;
        _movement.GravityEnabled = false;
        _jump.enabled = false;

        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
        }

        if (normal.x > 0)
        {
            Player.Instance.Rotator.SnapToRotation(-90);
        }
        else
        {
            Player.Instance.Rotator.SnapToRotation(90);
        }
        _rootingSFX.PlayOneShot(AudioManager.Instance.SfxSource);
        _jump.TriggerParticles();
        //rotate visuals to normal
    }

    public void LaunchFromRooted(Vector2 direction) 
    {
        IsRooted = false;
        _jump.HasJumped = true;
        Animator.SetBool("Rooted",false);
        _rb.velocity = direction * _launchPower;
        _movement.GravityEnabled = true;
        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
        }
        _movementCoroutine = StartCoroutine(ReEnableMovementAfterSeconds(_lockedMovementTime));
        if (direction.x >= 0)
        {
            Player.Instance.Rotator.TriggerFlip(true, true);
        }
        else
        {
            Player.Instance.Rotator.TriggerFlip(false, true);
        }
        _launchSFX.PlayOneShot(AudioManager.Instance.SfxSource);
        _jump.EnableFartTrail();
    }

    private IEnumerator ReEnableMovementAfterSeconds(float time) 
    {
        yield return new WaitForSeconds(time);
        _movement.MovementEnabled = true;
        _jump.enabled = true;
    }
}
