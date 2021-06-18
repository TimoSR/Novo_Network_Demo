using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Mirror;
using TreeEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")] 
    [SerializeField] private Vector2 maxfollowOffset = new Vector2(-1, 6f);
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private Controls _input;

    private Controls Input
    {
        get
        {
            if (_input != null) { return _input;}

            return _input = new Controls();
        }
    }

    private CinemachineTransposer _transposer;

    [ClientCallback]
    private void OnEnable() => Input.Enable();

    [ClientCallback]
    private void OnDisable() => Input.Disable();

    public override void OnStartAuthority()
    {
        _transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualCamera.gameObject.SetActive(true);

        enabled = true;
        
        Input.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }
    
    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;

        // Can't go below maxFollowOffset x or above maxFollowOffset y. 
        float followOffset = Mathf.Clamp(_transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * deltaTime), maxfollowOffset.x, maxfollowOffset.y);

        _transposer.m_FollowOffset.y = followOffset;

        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);

    }
    
}
