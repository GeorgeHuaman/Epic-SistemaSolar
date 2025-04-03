using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeActiveGap : MonoBehaviour
{
    public static ChangeActiveGap Instance;

    private List<GapObject> allGaps;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        GetCollections();
    }

    private void GetCollections()
    {
        allGaps = FindObjectsOfType<GapObject>().ToList();
    }

    public void ActivateRandomGap()
    {
        List<GapObject> availableGaps = allGaps.Where( x => x.GetFilled() == false && x.GetActive() == false ).ToList();

        if (!availableGaps.Any()) return;

        if (availableGaps.Count <= 1)
        {
            availableGaps.First().ActivateGap();
            return;
        }

        int randomGapNumber = Random.Range(0, availableGaps.Count);
        availableGaps[randomGapNumber].ActivateGap();
    }
}
