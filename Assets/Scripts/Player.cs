using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public PlayerInput Input { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerJump Jump { get; private set; }
    public PlayerRooting Rooting { get; private set; }
    public PlayerRotator Rotator { get; private set; }

    public InputActionReference RestartAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Input = GetComponent<PlayerInput>();
        Movement = GetComponent<PlayerMovement>();
        Jump = GetComponent<PlayerJump>();
        Rooting = GetComponent<PlayerRooting>();
        Rotator = GetComponent<PlayerRotator>();

        RestartAction.ToInputAction().performed += Restart;
    }

    private void Restart(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
