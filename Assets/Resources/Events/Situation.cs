using System.Collections.Generic;
using UnityEngine;

public enum SituationType
{
    Travelling,
    EnemyEncounter,
    POIEncounter
}

public abstract class Situation
{
    public SituationType type;
    public RectTransform expSituationPanel;
    public bool logIsUpdated, readyForNewSituation;

    string log;
    public string Log
    {
        set
        {
            logIsUpdated = false;
            log = value;
        }
        get
        {
            logIsUpdated = true;
            return log;
        }
    }

    public abstract void Update();
}