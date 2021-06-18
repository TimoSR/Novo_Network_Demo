using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Mirror;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    public override void OnStartAuthority()
    {
        virtualCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
}
