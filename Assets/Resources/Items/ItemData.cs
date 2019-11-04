using System;
using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Item Data")]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public int cost = 1;
    public int stackSize = 1;
    public int charges = 0;
    public StatsSheet equipStats;
    public EquipmentSlot equipmentSlot;
    public HeroClassType reqHeroClassType = HeroClassType.Any;
    [Reorderable(ReorderableNamingType.VariableValue, "statType")]
    public List<StatModifier> statModifiers;
    //[Reorderable(ReorderableNamingType.VariableValue, "effectOnStatsType")]
    //public List<Effect> useEffects;
}