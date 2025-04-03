using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivateAllOrbits : MonoBehaviour
{
    public static ActivateAllOrbits Instance;

    private List<ObjectOrbit> allOrbits;
    private List<GapObject> allGaps;

    [Header("Orbit Center Elevation")]
    [SerializeField] private float orbitCenterElevation;
    [SerializeField] private Transform orbitCenter;

    [Header("States")]
    [SerializeField] private bool winCondition;

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        winCondition = false;
        GetCollections();
    }

    private void GetCollections()
    {
        allOrbits = FindObjectsOfType<ObjectOrbit>().ToList();
        allGaps = FindObjectsOfType<GapObject>().ToList();
    }

    public void Check()
    {
        CheckAllGapFilled();
    }

    private void CheckAllGapFilled()
    {
        if (winCondition == true) return;

        foreach(GapObject gap in allGaps)
        {
            if (gap == null) continue;

            if (gap.GetFilled() == false) return;
        }

        winCondition = true;

        if (winCondition == true) ActivateOrbits();
    }

    private void ActivateOrbits()
    {
        allOrbits.ForEach(x => x.ToggleActive(true));
        orbitCenter.transform.position += Vector3.up * orbitCenterElevation;
    }
}
