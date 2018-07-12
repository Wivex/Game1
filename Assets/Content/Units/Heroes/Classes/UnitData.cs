using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data Templates/Unit Data")]
public class UnitData : ScriptableObject
{
    public UnitStats baseStats;
    public List<AbilityData> abilities;
}