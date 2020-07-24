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
    internal Encounter curEncounter;
    internal MissionRoute route;
    internal int sitesSinceLastEncounter = 0;

    public event Action SiteChanged;

    public bool GracePeriodPassed => sitesSinceLastEncounter > MissionsManager.i.minGracePeriod;

    internal Mission(MissionSetUp misSetUp)
    {
        hero = misSetUp.hero;
        route = new MissionRoute(misSetUp.path);
    }

    public void NextAction()
    {
        if (curEncounter != null)
            curEncounter.NextAction();
        else
            NextSite();
    }

    void NextSite()
    {
        SiteChanged();
        EncounterCheck();
    }

    void EncounterCheck()
    {
        if (GracePeriodPassed)
        {
            switch (route.curZone.encounters.PickOne().type)
            {
                case EncounterType.None:
                    curEncounter = null;
                    break;
                case EncounterType.Enemy:
                    curEncounter = new EnemyEncounter(this);
                    break;
            }
        }
    }

    public void KeepTravelling()
    {
        Debug.Log("KeepTravelling Trigger");
        //StartAnimation(AnimationTrigger.KeepTravelling, heroAM);
    }

    // UNDO: should move somewhere?
    internal void StartAnimation(AnimationTrigger trigger, params AnimationManager[] managers)
    {
        AnimationManager.Trigger(trigger, managers);
    }
}