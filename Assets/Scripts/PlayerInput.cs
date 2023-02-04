using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    public InputActionAsset inputSettings;
    // Start is called before the first frame upda
    
    private void OnEnable()
    {
        inputSettings.Enable();
    }

    private void OnDisable()
    {
        inputSettings.Disable();
    }
    
}
