using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public InputActionReference movementInput;
    public InputActionReference floatInput;
    public InputActionReference fastFallInput;
    
    private Rigidbody _rigidbody;

    public float speed;

    [Header("Gravity Stuff")]
    public bool GravityEnabled = true;
    [SerializeField] private float _neutralGravity = 9.82f;
    [SerializeField] private float _heavyGravity = 15f;
    [SerializeField] private float _lightGravity = 4f;
    [SerializeField] private float _gravityLerpFactor = 5f;
    [SerializeField] private float _currentGravity = 9.82f;

    private GravityState _playerGravity = GravityState.Neutral;
    private float _targetGravity = 9.82f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        floatInput.ToInputAction().performed += SetToLight;
        fastFallInput.ToInputAction().performed += SetToHeavy;
        floatInput.ToInputAction().canceled += SetToNeutral;
        fastFallInput.ToInputAction().canceled += SetToNeutral;
    }
    
    void Update()
    {
        Vector2 input = movementInput.action.ReadValue<Vector2>();
        input *= speed;
        Vector3 vdi = _rigidbody.velocity;
        _rigidbody.velocity = new Vector3(input.x,vdi.y,input.y);

        GravityCheck();
    }

    private void GravityCheck() 
    {
        switch (_playerGravity)
        {
            case GravityState.Neutral:
                _targetGravity = _neutralGravity;
                break;
            case GravityState.Light:
                _targetGravity = _lightGravity;
                break;
            case GravityState.Heavy:
                _targetGravity = _heavyGravity;
                break;
            default:
                break;
        }

        _currentGravity = Mathf.Lerp(_currentGravity, _targetGravity, Time.deltaTime * _gravityLerpFactor);
    }

    private void FixedUpdate()
    {
        if (GravityEnabled)
        {
            _rigidbody.AddForce(Vector3.down * _currentGravity);
        }
    }

    private void SetToNeutral(InputAction.CallbackContext obj) 
    {
        _playerGravity = GravityState.Neutral;
    }

    private void SetToLight(InputAction.CallbackContext obj)
    {
        _playerGravity = GravityState.Light;
    }

    private void SetToHeavy(InputAction.CallbackContext obj)
    {
        _playerGravity = GravityState.Heavy;
    }
}

public enum GravityState
{
    Neutral,
    Light,
    Heavy
}

