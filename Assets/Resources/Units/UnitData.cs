using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    public Sprite icon;
    public DataStats baseStats;
    [Reorderable(ReorderableNamingType.ObjectName)]
    public List<AbilityData> abilities;
    [Reorderable(ReorderableNamingType.VariableValue, "action.actionType")]
    public List<Tactic> tactics;
}