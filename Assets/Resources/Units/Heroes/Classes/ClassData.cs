using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Class Data")]
public class ClassData : UnityEngine.ScriptableObject
{
    public List<UnitData> classLevels;
    public List<int> expPerLevel;
}