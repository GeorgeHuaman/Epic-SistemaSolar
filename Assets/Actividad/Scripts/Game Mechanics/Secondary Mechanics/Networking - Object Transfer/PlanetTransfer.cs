using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTransfer : MonoBehaviour 
{
    //private void OnEnable()
    //{
    //    SpatialBridge.actorService.onActorJoined += SetTransferOwnership;
    //}

    //private void OnDisable()
    //{
    //    SpatialBridge.actorService.onActorJoined -= SetTransferOwnership;
    //}

    //private void SetTransferOwnership(ActorJoinedEventArgs args)
    //{
    //    IActor joinedActor = SpatialBridge.actorService.actors[args.actorNumber];

    //    joinedActor.avatar.onColliderHit += TransferOwnership;

    //    void TransferOwnership(ControllerColliderHit hit, Vector3 avatarVelocity)
    //    {
    //        PlanetNetworkObject planetNetwork = hit.gameObject.GetComponent<PlanetNetworkObject>();

    //        if (planetNetwork != null)
    //        {
    //            planetNetwork.Request(joinedActor);
    //        }
    //    }
    //}


}
