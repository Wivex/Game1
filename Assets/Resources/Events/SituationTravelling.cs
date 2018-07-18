using System.Collections.Generic;
using UnityEngine;

public class SituationTravelling : Situation
{
    public SituationTravelling(LocationData location)
    {
        Log = $"Travelling trough {location.name}";
        type = SituationType.Travelling;
        readyForNewSituation = true;
    }

    public override void Update()
    {
    }
}