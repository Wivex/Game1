using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability Data")]
public class AbilityData : ScriptableObject
{
    [Header("Ability")]
    public Sprite icon;
    public int cooldown;
    [Reorderable(ReorderableNamingType.VariableValue, "effectType")]
    public List<Effect> effects;
}