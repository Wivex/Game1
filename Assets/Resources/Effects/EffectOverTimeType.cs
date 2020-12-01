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
    //[Tooltip("Stack on itself each turn")]
    //public bool progressive;
    public int value;
}

[CreateAssetMenu(menuName = "Content/Data/Effect Over Time Type")]
public class EffectOverTimeType : ScriptableObject
{
    public Sprite icon;
    public GameObject procAnimationPrefab;
    public EffectInfluenceType influence;
    public EffectDirectType directEffect;
    public DamageType damageType;
    public bool stackable = true;
    public List<StatMod> statMods;

    internal bool IsNegative => influence == EffectInfluenceType.Negative;
}