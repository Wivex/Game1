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
    internal event Action<Unit, Damage> UnitTookDamage;
    internal event Action<Unit, EffectOverTime> EffectApplied, EffectRemoved;

    #endregion

    internal bool GracePeriodPassed => locationsSinceLastEncounter > MissionsManager.i.minGracePeriod;
    internal Combat Combat => curEncounter as Combat;

    internal Mission(MissionSetUp misSetUp)
    {
        hero = misSetUp.hero;
        route = new MissionRoute(misSetUp.path);
    }

    internal void NextUpdate()
    {
        if (curEncounter?.resolved != false) NextLocation();
        else curEncounter.EncounterUpdate();
    }

    void NextLocation()
    {
        route.NextLocation();
        LocationChanged?.Invoke();
        NextEncounter();
        EncounterStarted?.Invoke(curEncounter.type);
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
        }
    }

    #region UNIT OPERATIONS
    
    internal void ApplyDamage(Unit unit, Damage damage)
    {
        var protectionValue = 0;
        switch (damage.type)
        {
            case DamageType.Physical:
                protectionValue = unit.baseStats[StatType.Defence].ModdedValue;
                break;
        }
        var damAfterDR = Math.Max(damage.amount - protectionValue, 0);
        damage.amount = damAfterDR;
        unit.HP = Math.Max(unit.HP - damAfterDR, 0);

        UnitTookDamage?.Invoke(unit, damage);
    }
    
    internal void ApplyEffects(Unit unit)
    {
        unit.effectStacks.Remove(effect);
        EffectRemoved?.Invoke(unit, effect);
    }
    
    internal void RemoveEffect(Unit unit, EffectOverTime effect)
    {
        unit.effectStacks.Remove(effect);
        EffectRemoved?.Invoke(unit, effect);
    }

    #endregion
}