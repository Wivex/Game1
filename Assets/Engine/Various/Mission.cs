using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Mission
{
    internal Hero hero;
    internal MissionRoute route;
    internal NoEncounter curEncounter;
    internal int locationsSinceLastEncounter = 0;

    #region EVENTS

    internal event Action LocationChanged;
    internal event Action<EncounterType> EncounterStarted;

    #endregion

    internal bool GracePeriodPassed => locationsSinceLastEncounter > MissionsManager.i.minGracePeriod;
    internal Combat Combat => curEncounter as Combat;

    internal Mission(MissionSetUp misSetUp)
    {
        hero = misSetUp.hero;
        route = new MissionRoute(misSetUp.path);
    }

    internal void NextAction()
    {
        if (curEncounter?.resolved != false)
        {
            NextLocation();
            NextEncounter();
        }
        else curEncounter.NextEncounterAction();
    }

    void NextLocation()
    {
        route.NextLocation();
        LocationChanged?.Invoke();
    }

    void NextEncounter()
    {
        if (GracePeriodPassed || curEncounter == null)
        {
            switch (route.curZone.encounters.PickOne().type)
            {
                case EncounterType.None:
                    curEncounter = new NoEncounter(this);
                    // TODO: move to Init or smth
                    curEncounter.resolved = true;
                    break;
                case EncounterType.Combat:
                    curEncounter = new Combat(this);
                    break;
            }
            EncounterStarted?.Invoke(curEncounter.type);
        }
    }
}