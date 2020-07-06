using System.Collections.Generic;
using SubjectNerd.Utilities;

public abstract class UnitData : DataWithIcon
{
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