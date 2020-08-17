using System;
using System.Collections.Generic;
using UnityEngine;

public enum EffectInfluenceType
{
    Positive,
    Negative
}

[Serializable]
public class StatMod
{
    public StatType stat;
    public StatModType statModType;
    public int value;
}

[CreateAssetMenu(menuName = "Content/Data/Effect Over Time Type")]
public class EffectOverTimeType : ScriptableObject
{
    public Sprite icon;
    public GameObject animationPrefab;
    public EffectInfluenceType influence;
    public DamageType damageType;
    public List<StatMod> statMods;
}