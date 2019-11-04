using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEditor;
using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    public Sprite icon;
    public StatsSheet stats;
    public EnergyType energyType;
    [Reorderable(ReorderableNamingType.ObjectName)]
    public List<AbilityData> abilities;
    [Reorderable(ReorderableNamingType.VariableValue, "action.actionType")]
    public List<Tactic> tactics;

    // UNDONE: errors on create new SO
    // TODO: optimize, to avoid sorting all objects each validation
    // sort ascending by drop chance, for easier loot spawning
    void OnEnable()
    {
        var assetPath = AssetDatabase.GetAssetPath(this);
        //"Assets/Resources/Units/Heroes/Classes/Warrior/WarriorClass.asset"
        var startIndex = "Assets/Resources/".Length;
        var endIndex = assetPath.IndexOf(".asset");
        var resourcePath = assetPath.Substring(startIndex, endIndex-startIndex);
        //"Units/Heroes/Classes/Warrior/WarriorClass"
        icon = Resources.Load<Sprite>(resourcePath);
    }
}