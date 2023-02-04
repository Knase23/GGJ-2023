using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public InputActionReference movementInput;
    
    private Rigidbody _rigidbody;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector2 input = movementInput.action.ReadValue<Vector2>();
        input *= speed;
        Vector3 vdi = _rigidbody.velocity;
        _rigidbody.velocity = new Vector3(input.x,vdi.y,input.y);
    }
    
}

