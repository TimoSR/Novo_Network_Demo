using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{

    [SerializeField] private Vector3 movement = new Vector3();

    [Client]
    private void Update()
    {

        if (!hasAuthority)
        {
            return;
        }
        
        if (!Input.GetKeyDown(KeyCode.W))
        {
            return;
        }
        
        transform.Translate(movement);
        
    }
}
