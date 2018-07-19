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
    public bool newLogEntry, readyForNewSituation;

    string log;
    public string Log
    {
        set
        {
            newLogEntry = true;
            log = value;
        }
        get
        {
            newLogEntry = false;
            return log;
        }
    }

    public abstract void Update();
}