using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Mirror;
using UnityEngine;

public class TargetQR: NetworkBehaviour
{

    private string _objectName;
    // Belongs somewhere
    private GameObject _target;
    private Vector3 _targetTransform;
    private Quaternion _targetRotation;
    [SerializeField] private GameObject _local= null;
    [SerializeField] private GameObject _originalParent = null;
    [SerializeField] private GameObject _targetChild= null;

    [Client]
    public void GoToTarget()
    {

        _objectName = gameObject.name;

        _objectName = _objectName + "Target";

        _target = GameObject.Find(_objectName);

        //Debug.Log(_objectName);

        _targetTransform = _target.transform.position;

        _targetRotation = _target.transform.rotation;

        TempParent();

        //Debug.Log(_objectName + " transform is: " + _targetTransform);

        //Debug.Log(_objectName + " quaternion rotation is: " + _targetRotation);

        _local.transform.position = _targetTransform;

        _local.transform.rotation = _targetRotation;

        ReturnParent();

    }

    [Client]
    private void TempParent()
    {
        _local.transform.parent = null;

        _targetChild.transform.SetParent(_local.transform);

    }

    [Client]
    private void ReturnParent()
    {
        _targetChild.transform.parent = null;

        _local.transform.SetParent(_originalParent.transform);
    }

}