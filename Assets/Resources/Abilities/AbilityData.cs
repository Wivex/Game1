using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability Data")]
public class AbilityData : DataWithIcon
{
    public int cooldown;
    [Reorderable(ReorderableNamingType.VariableValue, "effectType")]
    public List<Effect> effects;
}