using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingInteractable : MonoBehaviour
{
    [SerializeField] private RollableObject rollingObject;
    [SerializeField] private PedestalInteraction pedestal;
    [SerializeField] private bool isActive;

    public void StartRolling()
    {
        if (isActive == false)
        {
            SpatialBridge.coreGUIService.DisplayToastMessage("Parece que este no es el planeta que busco");
            return;
        }

        RollActionManager.instance.StartRoll(rollingObject);
        pedestal.Dissapear();
    }

    public void ToggleActive(bool isActive)
    {
        this.isActive = isActive;
    }

}
