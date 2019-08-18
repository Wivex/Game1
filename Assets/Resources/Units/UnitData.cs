using System.Collections.Generic;
using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    public Sprite icon;
    public UnitStats stats;
    public List<AbilityData> abilities;
    public TacticsPreset tacticsPreset;
}