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

    public override void OnStartAuthority()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        virtualCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;

        enabled = true;

        InputManager.Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }

    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;
        
        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime , 0f);
        
    }
}
