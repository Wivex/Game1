using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Unit Data")]
public class UnitData : ScriptableObject
{
    [Header("Unit Data")]
    public Sprite icon;
    //public StatSheet stats;
    public List<AbilityData> abilities;
    public TacticsPreset tacticsPreset;
}