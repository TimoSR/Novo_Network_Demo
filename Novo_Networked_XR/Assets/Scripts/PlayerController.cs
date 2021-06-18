using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    [Header("Components")]
    //public NavMeshAgent agent;
    [SerializeField]
    private NavMeshAgent agent;
    
    [Header("Movement")]
    public float rotationSpeed = 100;
    
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private InputManager _inputManager;
    private Transform cameraTransform;
    private CharacterController controller;

    [Client]
    private void Start()
    {
        // Checking if authority
        if (!hasAuthority) return;
        
        // movement for local player
        if (!isLocalPlayer) return;

        controller = GetComponent<CharacterController>();
        _inputManager = InputManager.Instance;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    
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
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        agent.velocity = forward * Mathf.Max(vertical, 0) * agent.speed;
    
    }
    
    // [Client]
    // void Update()
    // {
    //     groundedPlayer = controller.isGrounded;
    //     if (groundedPlayer && playerVelocity.y < 0)
    //     {
    //         playerVelocity.y = 0f;
    //     }
    //
    //     Vector2 movement = _inputManager.GetPlayerMovement();
    //     
    //     var directiveForceX = movement.x;
    //     var directiveForceZ = movement.y;
    //     
    //     Vector3 move = new Vector3(directiveForceX, 0, directiveForceZ);
    //     controller.Move(move * Time.deltaTime * playerSpeed);
    //
    //     // if (move != Vector3.zero)
    //     // {
    //     //     gameObject.transform.forward = move;
    //     // }
    //
    //     // Changes the height position of the player..
    //     if (_inputManager.PlayerJumped() && groundedPlayer)
    //     {
    //         playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //     }
    //
    //     playerVelocity.y += gravityValue * Time.deltaTime;
    //     controller.Move(playerVelocity * Time.deltaTime);
    // }

    // [Header("Movement")]
    // public float rotationSpeed = 100;
    //
    //
    // [Client]
    // void Update()
    // {
    //     
    //     // Checking if authority
    //     if (!hasAuthority) return;
    //
    //     // movement for local player
    //     if (!isLocalPlayer) return;
    //
    //     // rotate
    //     float horizontal = Input.GetAxis("Horizontal");
    //     transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);
    //
    //     // move
    //     float vertical = Input.GetAxis("Vertical");
    //     Vector3 forward = transform.TransformDirection(Vector3.forward);
    //     agent.velocity = forward * Mathf.Max(vertical, 0) * agent.speed;
    //
    // }
    //
    // // Called From Client 
    //
    // [Command]
    // private void CmdMove()
    // {
    //     
    //     // None safe 
    //     
    //     RpcMove();
    // }
    //
    // //Running on Server
    //
    // private void RpcMove()
    // {
    //     
    // }
}
