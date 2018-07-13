using System;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    Head,
    Body,
    Arms,
    Boots,
    Amulet,
    Ring,
    MainHand,
    OffHand
}

[CreateAssetMenu(menuName = "Content/Data Templates/Items/Equipment Data")]
public class EquipmentData : ItemData
{
    [Header("Equipment")] public int cost;
    public EquipmentSlot slot;
    public bool classRestricted;
    [HiddenIfNot("classRestricted")] public HeroClass reqClass;

    public List<StatModifier> statModifiers;

    //public void Equip(Hero hero)
    //{
    //    foreach (var effect in effects)
    //        effect.OnEquip(hero);
    //}

    //public void Unequip(Hero hero)
    //{
    //    foreach (var effect in effects)
    //        effect.OnUnequip(hero);
    //}
}