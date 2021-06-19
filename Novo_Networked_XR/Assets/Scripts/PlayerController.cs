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
    //[SerializeField] private NavMeshAgent agent;
    [SerializeField] private CharacterController controller = null;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float movementSpeed = 5.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Transform cameraTransform;
    private InputManager inputManager;
    private Vector2 previousInput;
    private Controls _playerControls;

    public override void OnStartAuthority()
    {
        controller = GetComponent<CharacterController>();
        
        InputManager.Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        InputManager.Controls.Player.Move.canceled += ctx => ResetMovement();
    }

    [ClientCallback]
    private void Update() => Move();

    [Client]
    private void SetMovement(Vector2 movement) => previousInput = movement;

    [Client]
    private void ResetMovement() => previousInput = Vector2.zero;
    
    [Client]
    private void Move()
    {
        var transform1 = controller.transform;
        Vector3 right = transform1.right;
        Vector3 forward = transform1.forward;
        right.y = 0f;
        forward.y = 0f;

        Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;

        controller.Move(movement * movementSpeed * Time.deltaTime);
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
