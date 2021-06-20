using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class ClickableMeta : MonoBehaviour
{

    [field: SerializeField] public UnityEvent onClick;
    
    public void OnClick()
    {
        onClick?.Invoke();
        Debug.Log("Click registered");
    }

}
