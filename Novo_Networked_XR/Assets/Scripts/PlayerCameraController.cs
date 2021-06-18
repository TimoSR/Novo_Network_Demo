using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Mirror;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerCameraController : NetworkBehaviour
{
    
    [Header("Camera")]
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;
    
    private InputManager inputManager;

    // private Controls controls;
    // private Controls Controls
    // {
    //     get
    //     {
    //         if (controls != null) { return controls; }
    //         return controls = new Controls();
    //     }
    // }

    public override void OnStartAuthority()
    {
        
        // Checking if authority
        if (!hasAuthority) return;
        
        // movement for local player
        if (!isLocalPlayer) return;
        
        virtualCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
        
        inputManager = InputManager.Instance;

        //Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }
    
    [Client]
    public void Update()
    {
        Look(inputManager.GetPointerDelta());
    }

    // [ClientCallback]
    // private void OnEnable() => Controls.Enable();
    // [ClientCallback]
    // private void OnDisable() => Controls.Disable();

    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;
        
        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
    }
    
    // Save for later
    
    // [Header("Camera")]
    // [SerializeField] private CinemachineVirtualCamera virtualCamera = null;
    //
    // public override void OnStartAuthority()
    // {
    //     virtualCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
    // }
}
