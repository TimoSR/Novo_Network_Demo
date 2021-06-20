using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class ClickableQR : NetworkBehaviour
{

    [field: SerializeField] public UnityEvent onClick;

    [Server]
    public void OnClick()
    {
        onClick?.Invoke();
        Debug.Log("Click registered");
    }

}
