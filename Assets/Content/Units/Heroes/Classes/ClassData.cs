using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data Templates/Classes/Class Data")]
public class ClassData : ScriptableObject
{
    public List<UnitData> classLevels;
}