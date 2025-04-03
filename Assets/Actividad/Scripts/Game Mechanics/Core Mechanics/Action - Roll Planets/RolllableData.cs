using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rollable", menuName = "ScriptableObjects/RollableData")]
public class RolllableData : ScriptableObject
{
    public float rollingRotationalSpeed;
    public float rollingRotationMultiplier;
    public float rollingSpeed;
    public float rollingAcceleration;
    public float rollingFriction;
    public float objectMass;
    public float objectRadius;
}
