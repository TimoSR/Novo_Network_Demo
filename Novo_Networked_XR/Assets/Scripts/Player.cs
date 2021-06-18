using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Player : NetworkBehaviour
{
    
    [Header("Components")]
    public NavMeshAgent agent;

    [Header("Movement")]
    public float rotationSpeed = 100;

    [Client]
    void Update()
    {
        
        // Checking if authority
        if (!hasAuthority) return;

        // movement for local player
        if (!isLocalPlayer) return;

        // rotate
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

        // move
        CmdMove();

    }
    
    // Called From Client 
    
    [Command]
    private void CmdMove()
    {
        
        // None safe 
        
        RpcMove();
    }
    
    //Running on Server
    
    private void RpcMove()
    {
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        agent.velocity = forward * Mathf.Max(vertical, 0) * agent.speed;
    }
}
