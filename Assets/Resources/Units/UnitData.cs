using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Unit Data")]
public class UnitData : ContentData
{
    [Header("Unit Data")]
    public StatValues stats;
    [Reorderable]
    public List<AbilityData> abilities;
    public TacticsPreset tacticsPreset;
}