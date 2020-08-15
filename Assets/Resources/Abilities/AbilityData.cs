using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability Data")]
public class AbilityData : ScriptableObject
{
    public Sprite icon;
    public GameObject animationPrefab;
    public int cooldown;
    public int energyCost;
    public EnergyType energyType;
    public List<EffectData> effects;
}