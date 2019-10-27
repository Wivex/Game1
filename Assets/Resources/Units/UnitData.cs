using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    public Sprite icon;
    public UnitStats stats;
    [Reorderable(ReorderableNamingType.ObjectName)]
    public List<AbilityData> abilities;
    [Reorderable(ReorderableNamingType.VariableValue, "action.actionType")]
    public List<Tactic> tactics;
}