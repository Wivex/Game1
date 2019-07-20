using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability Data")]
public class AbilityData : ScriptableObject
{
    [Header("Ability")]
    public Sprite icon;
    public int cooldown;
    public List<Effect> effects;
}