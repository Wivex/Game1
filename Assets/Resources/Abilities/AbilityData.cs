using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability Data")]
public class AbilityData : ScriptableObject
{
    public Sprite icon;
    public int cooldown;
    public EnergyType energyType;
    public int energyCost;
    public List<Effect> effects;
}