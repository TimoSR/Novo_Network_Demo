using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : NetworkBehaviour
{
    
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    
    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
    
    //Singleton 

    public static InputManager Instance { get; private set; }

    public static Controls Controls { get; private set; }

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
        
        if (Controls != null) { return; }
        
        Controls= new Controls();

    }
    
    [ClientCallback]
    private void OnEnable() => Controls.Enable();

    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    private Vector2 GetPlayerMovement()
    {
        return Controls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetPointerDelta()
    {
        return Controls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return Controls.Player.Jump.triggered;
    }
    public void OnLook(InputValue value)
    {
        if(cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }
    
    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

}
