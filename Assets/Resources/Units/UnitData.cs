using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Unit Data")]
public class UnitData : ContentData
{
    [Header("Unit Data")]
    public StatValues stats;
    public List<AbilityData> abilities;
    public TacticsPreset tacticsPreset;
}