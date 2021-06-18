using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Agent/MovementData")]

public class SOAgentMovementData : ScriptableObject
{
    
    [Range(1,10)]
    public float startVelocity  = 5;
    
    [Range(1,10)]
    public float maxSpeed = 5;

    [Range(0.1f, 100)]
    public float acceleration = 50, deAcceleration = 50;
    
}
