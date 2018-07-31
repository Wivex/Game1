using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability Data")]
public class AbilityData : ContentData
{
    [Header("Ability")]
    public int cooldown;
    [Reorderable("Effect")]
    public List<Effect> effects;
}