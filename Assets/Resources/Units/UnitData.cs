using System.Collections.Generic;
using SubjectNerd.Utilities;
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
    [Reorderable(ReorderableNamingType.ReferencedObjectName)]
    public List<AbilityData> abilities;
    [Reorderable(ReorderableNamingType.VariableValue, "action.actionType")]
    public List<Tactic> tactics;
}