using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;
    private Camera mainCamera;

    private Controls controls;
    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public override void OnStartAuthority()
    {
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        virtualCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;

        enabled = true;

        Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());

        Controls.Player.Click.performed += _ => EndedClick();

    }

    private void EndedClick()
    {
        CmdDetectObject();
    }

    [Command]
    private void CmdDetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Controls.Player.TargetPosition.ReadValue<Vector2>());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                Debug.Log("3D Hit" + hit.collider.tag);
            }   
        }

    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;

        virtualCamera.transform.Rotate(-lookAxis.y * cameraVelocity.x * deltaTime,0f, 0f);

        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
    }
}
