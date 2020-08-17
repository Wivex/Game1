using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Effect Over Time Type")]
public class EffectOverTimeType : ScriptableObject
{
    public Sprite icon;
    public GameObject animationPrefab;
    public DamageType damageType;
    public StatType stat;
    public StatModType statModType;
}