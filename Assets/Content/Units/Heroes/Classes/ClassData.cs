using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data Templates/Classes/Class")]
public class ClassData : ScriptableObject
{
    public string className;
    public Sprite icon;
    public List<UnitData> classLevels;
}
