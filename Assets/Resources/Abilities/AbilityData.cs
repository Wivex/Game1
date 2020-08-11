using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability Data")]
public class AbilityData : ScriptableObject
{
    public Sprite icon;
    public GameObject abilityPrefab;
    public int cooldown;
    public EnergyType energyType;
    public int energyCost;
    public List<EffectData> effects;
}