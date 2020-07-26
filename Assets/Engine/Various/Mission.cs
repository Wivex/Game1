using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum AnimationTrigger
{
    KeepTravelling,
    BeginEncounter,
    EndEncounter,
    StartTransferLoot,
    Attack,
    TakeDamage
}

public class Mission
{
    internal Hero hero;
    internal MissionRoute route;
    internal NoEncounter curEncounter;
    internal int locationsSinceLastEncounter = 0;

    #region region EVENTS

    internal event Action LocationChanged;
    internal event Action<EncounterType> EncounterStarted;

    #endregion

    internal bool GracePeriodPassed => locationsSinceLastEncounter > MissionsManager.i.minGracePeriod;

    internal Mission(MissionSetUp misSetUp)
    {
        hero = misSetUp.hero;
        route = new MissionRoute(misSetUp.path);
    }

    internal void NextAction()
    {
        if (curEncounter != null)
            curEncounter.NextAction();
        else
            NextLocation();
    }

    void NextLocation()
    {
        LocationChanged?.Invoke();
        NextEncounter();
    }

    void NextEncounter()
    {
        if (GracePeriodPassed)
        {
            switch (route.curZone.encounters.PickOne().type)
            {
                case EncounterType.None:
                    curEncounter = new NoEncounter(this);
                    break;
                case EncounterType.Enemy:
                    curEncounter = new EnemyEncounter(this);
                    break;
            }

            EncounterStarted?.Invoke(curEncounter.type);
        }
    }
}