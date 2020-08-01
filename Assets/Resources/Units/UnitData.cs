using System.Collections.Generic;
using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    [Header("Unit Properties")]
    public StatsSheet baseStats = new StatsSheet
    {
        health = 100,
        energy = 100,
        attack = 10,
        defence = 5,
        speed = 10
    };
    public EnergyType energyType = EnergyType.Mana;
    public List<AbilityData> abilities;
    public List<Tactic> tactics;

    internal virtual void OnValidate()
    {
        foreach (var tactic in tactics)
        {
            // can't be tactic without any trigger
            if (!tactic.triggers.NotNullOrEmpty())
                tactic.triggers = new List<TacticTrigger>
                {
                    new TacticTrigger
                    {
                        triggerType = TriggerType.Any
                    }
                };
        }
    }
}