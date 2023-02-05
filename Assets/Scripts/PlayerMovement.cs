using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public InputActionReference movementInput;
    public InputActionReference floatInput;
    public InputActionReference fastFallInput;
    
    private Rigidbody _rigidbody;
    public float speed;
    public float deadZone = 0.35f;
    public bool MovementEnabled = true;
    [Header("Gravity Stuff")]
    public bool GravityEnabled = true;
    [SerializeField] private float _neutralGravity = 9.82f;
    [SerializeField] private float _heavyGravity = 15f;
    [SerializeField] private float _lightGravity = 4f;
    [SerializeField] private float _gravityLerpFactor = 5f;
    [SerializeField] private float _currentGravity = 9.82f;

    private GravityState _playerGravity = GravityState.Neutral;
    private float _targetGravity = 9.82f;

    private List<GravityState> _heldButtons = new List<GravityState>();

    private bool _wallRight = false;
    private bool _wallLeft = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        floatInput.ToInputAction().performed += FloatButtonDown;
        fastFallInput.ToInputAction().performed += FallButtonDown;
        floatInput.ToInputAction().canceled += SetToNeutral;
        fastFallInput.ToInputAction().canceled += SetToNeutral;
    }
    
    void Update()
    {
        WallCheck();
        Vector2 input = movementInput.action.ReadValue<Vector2>();
        if (_wallLeft && input.x < 0) input.x = 0f;
        else if (_wallRight && input.x > 0) input.x = 0f;
        if (input.x > deadZone || input.x < -deadZone)
        {
            input *= speed;
            Vector3 vdi = _rigidbody.velocity;
            
            if (Mathf.Abs(vdi.x) > Mathf.Abs(input.x))
            {
                if ((input.x > 0 && vdi.x > 0) || (input.x < 0 && vdi.x < 0))
                {
                    input = new Vector2(vdi.x, 0f);
                }
            }
            if (MovementEnabled)
            {
                _rigidbody.velocity = new Vector3(input.x, vdi.y, 0);
            }
        }

        GravityCheck();
    }

    private void WallCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.right, 0.6f))
        {
            _wallRight = true;
        }
        else
        {
            _wallRight = false;
        }
        if (Physics.Raycast(transform.position, Vector3.left, 0.6f))
        {
            _wallLeft = true;
        }
        else
        {
            _wallLeft = false;
        }
    }

    private void GravityCheck() 
    {
        switch (_playerGravity)
        {
            case GravityState.Neutral:
                _targetGravity = _neutralGravity;
                break;
            case GravityState.Light:
                _targetGravity = _neutralGravity;
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
        if (_heldButtons.Count == 0)
        {
            _playerGravity = GravityState.Neutral;
        }
    }

    private void FloatButtonDown(InputAction.CallbackContext obj)
    {
        _heldButtons.Add(GravityState.Light);
        _playerGravity = GravityState.Light;
    }
    private void FallButtonDown(InputAction.CallbackContext obj)
    {
        _heldButtons.Add(GravityState.Heavy);
        _playerGravity = GravityState.Heavy;
    }
    private void FloatButtonUp(InputAction.CallbackContext obj)
    {
        _heldButtons.Remove(GravityState.Light);
        if (_heldButtons.Count == 0)
        {
            _playerGravity = GravityState.Neutral;
        }
    }
    private void FallButtonUp(InputAction.CallbackContext obj)
    {
        _heldButtons.Remove(GravityState.Heavy);
        if (_heldButtons.Count == 0)
        {
            _playerGravity = GravityState.Neutral;
        }
    }

}

public enum GravityState
{
    Neutral,
    Light,
    Heavy
}

