using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : NetworkBehaviour
{
    //Singleton 

    public static InputManager Instance { get; private set; }

    private Controls _playerControls;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);   
        }
        else
        {
            Instance = this;
        }
        
        _playerControls = new Controls();

    }

    [ClientCallback]
    private void OnEnable() => _playerControls.Enable();

    [ClientCallback]
    private void OnDisable() => _playerControls.Disable();

    public Vector2 GetPlayerMovement()
    {
        return _playerControls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetPointerDelta()
    {
        return _playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return _playerControls.Player.Jump.triggered;
    }
}
