using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapObject : MonoBehaviour
{
    [SerializeField] private RollableObject assignedPlanet;

    [Header("States")]
    [SerializeField] private bool isActive;
    [SerializeField] private bool isFilled;

    [Header("Properties")]
    [SerializeField] private float fillGapRadius;

    [Header("Visual")]
    [SerializeField] private GameObject activeGap;
    [SerializeField] private GameObject filledGap;

    private void Start()
    {
        if (assignedPlanet != null) assignedPlanet.GetInteractable().ToggleActive(isActive);

        ResolveVisual();
    }

    private void Update()
    {
        CheckFillGap();
    }

    public void ActivateGap()
    {
        if (isActive == true || isFilled == true) return;

        isActive = true;
        if(assignedPlanet != null) assignedPlanet.GetInteractable().ToggleActive(true);
        ResolveVisual();
    }

    public void FillGap()
    {
        if(isFilled == true) return;

        isFilled = true;
        if(assignedPlanet != null) assignedPlanet.gameObject.SetActive(false);
        ResolveVisual();

        ActivateAllOrbits.Instance.Check();
        ChangeActiveGap.Instance.ActivateRandomGap();
    }

    private void CheckFillGap()
    {
        if(isActive == false || isFilled == true) return;
        float distanceToAssignedPlanet = Vector3.Distance(transform.position, assignedPlanet.transform.position);

        if (distanceToAssignedPlanet <= fillGapRadius + assignedPlanet.data.objectRadius)
        {
            FillGap();
        }
    }

    private void ResolveVisual()
    {
        
        if (filledGap == null || activeGap == null) return;

        if(isFilled == true)
        {
            filledGap.SetActive(true);
            activeGap.SetActive(false);
        } else if(isActive == true)
        {
            filledGap.SetActive(false);
            activeGap.SetActive(true);
        } else
        {
            filledGap.SetActive(false);
            activeGap.SetActive(false);
        }
    }

    public bool GetFilled()
    {
        return isFilled; 
    }

    public bool GetActive()
    {
        return isActive;
    }
}
