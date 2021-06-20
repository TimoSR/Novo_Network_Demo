using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class ClickableQR : NetworkBehaviour
{

    [field: SerializeField] public UnityEvent onClick;
    private string _objectName;
    // Belongs somewhere
    private GameObject _target;

    private void Start()
    {
        _objectName = gameObject.name;
        _objectName = _objectName + "Target";
        
        Debug.Log(_objectName);
        
        _target = GameObject.Find(_objectName);
    }

    [Server]
    public void OnClick()
    {
        onClick?.Invoke();
        Debug.Log("Click registered");
    }

}
