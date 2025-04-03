using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollActionManager : MonoBehaviour
{
    private const float MIN_VELOCITY_TO_PUSH = .25f;

    public static RollActionManager instance;
    private IAvatar localAvatar;
    public RollingControls controls;

    [Header("Action Parameters")]
    [SerializeField] private float maxDistanceToRollingObject;
    [SerializeField] private float maxPushDistance;
    [SerializeField] private float pushStrength;
    [SerializeField] private float rangePushMultiplier;

    [Header("States")]
    [SerializeField] private bool isRolling;
    [SerializeField] private RollableObject rollingObject;

    private void Awake()
    {
        if (instance != null) return;

        instance = this;

        localAvatar = SpatialBridge.actorService.localActor.avatar;
    }

    private void Update()
    {
        if (isRolling == false) return;
        
        CheckValidRollOngoing();
    }

    private void FixedUpdate()
    {
        PushRollingObject();
    }

    private void PushRollingObject()
    {
        if (isRolling == false) return;

        Vector3 velocity = localAvatar.velocity;
        float distanceToRollingObject = Vector3.Distance(localAvatar.position, rollingObject.transform.position);

        float maxPushDistanceToActualRollingObject = maxPushDistance + rollingObject.data.objectRadius;

        if (distanceToRollingObject > maxPushDistanceToActualRollingObject) return;

        Vector3 directionToObject = (rollingObject.transform.position - localAvatar.position).normalized;

        float distanceInverseMultiplier = (distanceToRollingObject > 1f ? distanceToRollingObject : 1f) * rangePushMultiplier;

        if (velocity.magnitude > MIN_VELOCITY_TO_PUSH) rollingObject.PushRollingObject(directionToObject, pushStrength * velocity.magnitude / distanceInverseMultiplier);
    }

    public void StartRoll(RollableObject rollingObject)
    {
        if(rollingObject == null) return;
        else if(rollingObject.isActiveAndEnabled == false) return;

        if (controls != null)
        {
            controls.StartRollingControls();
        }

        this.rollingObject = rollingObject;
        rollingObject.StartRolling();
        isRolling = true;
    }

    public void EndRoll()
    {
        if(controls != null)
        {
            controls.StopRollingControls();
        }

        if (rollingObject != null) rollingObject.EndRolling();

        this.rollingObject = null;
        isRolling= false;
    }

    private void CheckValidRollOngoing()
    {
        if (rollingObject == null) 
        {
            EndRoll();
            return;
        }

        if (rollingObject.isActiveAndEnabled == false) 
        { 
            EndRoll();
            return;
        }

        float distanceToRollingObject = Vector3.Distance(localAvatar.position, rollingObject.transform.position);

        if(distanceToRollingObject > maxDistanceToRollingObject)
        {
            EndRoll();
        }
    }

    public IAvatar GetLocalAvatar()
    {
        return localAvatar;
    }
}
