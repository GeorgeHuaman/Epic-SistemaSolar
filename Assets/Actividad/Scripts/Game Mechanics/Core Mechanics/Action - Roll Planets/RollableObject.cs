using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollableObject : MonoBehaviour
{
    private const float MIN_VELOCITY = .25f;
    private const float GRAVITY_SPEED = -5f;

    public RolllableData data;
    [SerializeField] private RollingInteractable interactable;

    [Header("States")]
    [SerializeField] private bool isRolling;
    [SerializeField] private bool isGrounded;
    private bool isMoveFrameFlag = false;
    private Vector3 movementRollComponent = Vector3.zero;
    private Vector3 movementGravityComponent = Vector3.zero;

    [Header("Components - Animation")]
    [SerializeField] private GameObject rollingGameObject;

    [Header("States - Roll Movement")]
    [SerializeField] private Vector3 rollVelocity;

    [Header("Components - Movement")]
    [SerializeField] private Rigidbody _rigidbody;

    private void Update()
    {
        if(rollVelocity.magnitude < MIN_VELOCITY) return;
        
        AnimateRoll();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        CalculateGravity();

        if (rollVelocity.magnitude >= MIN_VELOCITY)
        {
            if (isMoveFrameFlag == false)
            {
                ApplyFriction();
            }

            CaculateRoll();
        }

        Move();

        isMoveFrameFlag = false;
        ResetMovementComponents();
    }

    public void StartRolling()
    {
        isRolling = true;
        _rigidbody.isKinematic = false;
        interactable.gameObject.SetActive(false);
    }

    public void PushRollingObject(Vector3 direction, float pushStrength)
    {
        if (isRolling == false) return;

        float pushImpulseMultiplier = pushStrength / data.objectMass;
        float actualAcceleration = data.rollingAcceleration * pushImpulseMultiplier;

        Vector3 flattenVelocity = direction;
        flattenVelocity.y = 0f;

        this.rollVelocity += flattenVelocity * actualAcceleration * Time.fixedDeltaTime;

        isMoveFrameFlag = true;
    }

    private void ApplyFriction()
    {
        rollVelocity -= rollVelocity * data.rollingFriction * Time.fixedDeltaTime;

        if (rollVelocity.magnitude < MIN_VELOCITY) rollVelocity = Vector3.zero;
    }

    public void EndRolling()
    {
        isRolling = false;
        interactable.gameObject.SetActive(true);
    }

    private void AnimateRoll()
    {
        if (rollingGameObject == null) return;

        if(rollVelocity != Vector3.zero) transform.forward = rollVelocity.normalized;

        float velocityRotationMultiplier = (rollVelocity.magnitude > 1f ? rollVelocity.magnitude : 1f) * data.rollingRotationMultiplier;

        rollingGameObject.transform.rotation *= Quaternion.Euler(data.rollingRotationalSpeed * velocityRotationMultiplier * Time.deltaTime, 0f, 0f);
    }

    private void CaculateRoll()
    {
        if (_rigidbody == null || rollVelocity.magnitude < MIN_VELOCITY) return;

        if (rollVelocity.magnitude > data.rollingSpeed) rollVelocity = rollVelocity.normalized * data.rollingSpeed;

        movementRollComponent = rollVelocity;
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position - Vector3.up * .05f, -Vector3.up,.05f);
    }

    private void CalculateGravity()
    {
        if (isGrounded) return;

        movementGravityComponent = Vector3.up * GRAVITY_SPEED;
    }

    private void ResetMovementComponents()
    {
        movementRollComponent = Vector3.zero;
        movementGravityComponent = Vector3.zero;
    }

    private void Move()
    {
        if (movementRollComponent == Vector3.zero && movementGravityComponent == Vector3.zero) return;

        Vector3 velocity = movementRollComponent + movementGravityComponent;

        _rigidbody.velocity = velocity;
    }

    public RollingInteractable GetInteractable()
    {
        return interactable;
    }
}
