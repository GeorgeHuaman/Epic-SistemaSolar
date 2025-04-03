using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalInteraction : MonoBehaviour
{
    private const float DESCRIPTION_MESSAGE_DURATION = 6f;

    [TextArea, SerializeField] private string planetDescription;

    public void Dissapear()
    {
        if (gameObject.activeSelf == false) return;

        gameObject.SetActive(false);
        SpatialBridge.coreGUIService.DisplayToastMessage(planetDescription, DESCRIPTION_MESSAGE_DURATION);
    }
}
