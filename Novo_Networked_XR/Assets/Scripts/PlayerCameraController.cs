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

    private GameInput _input;
    private CinemachineTransposer _transposer;

    public override void OnStartAuthority()
    {
        _transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualCamera.gameObject.SetActive(true);

        enabled = true;

        _input = new GameInput();
        _input.Player.Look.performed += context => Look 
 
    }
}
