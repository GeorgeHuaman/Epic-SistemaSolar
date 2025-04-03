using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrbit : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private Transform orbitAround;
    [SerializeField] private float orbitSpeedAnglePerSecond;
    [SerializeField] private int orbitDirection = 1;

    [Header("Control")]
    [SerializeField] private bool isActive;

    private void Update()
    {
        Orbit();
    }

    private void Orbit()
    {
        if (isActive == false || orbitAround == null) return;
    
        Vector3 direction = transform.position - orbitAround.position;
        Vector3 directionFlatten = direction;
        directionFlatten.y = 0f;
        directionFlatten.Normalize();
        float angle = Vector2.SignedAngle(Vector2.right, new Vector2(directionFlatten.x, directionFlatten.z));
        int orbitDirectionVerified = (int) Mathf.Sign(orbitDirection);
        if(orbitDirectionVerified == 0) orbitDirectionVerified = 1;

        angle += orbitSpeedAnglePerSecond * orbitDirectionVerified * Time.deltaTime;

        Vector3 distanceVector = direction;
        distanceVector.y = 0f;
        float distance = distanceVector.magnitude;
        float height = orbitAround.position.y;

        float newPositionXComponent =  Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
        float newPositionZComponent =  Mathf.Sin(angle * Mathf.Deg2Rad) * distance;
        Vector3 orbitAroundPositionFlatten = orbitAround.position;
        orbitAroundPositionFlatten.y = 0f;

        transform.position = new Vector3(newPositionXComponent, height, newPositionZComponent) + orbitAroundPositionFlatten;
    }

    public void ToggleActive(bool isActive)
    {
        this.isActive = isActive;
    }
}
