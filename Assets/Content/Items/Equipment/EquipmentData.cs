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

[CreateAssetMenu(menuName = "Content/Data Templates/Equipment")]
public class EquipmentData : ItemData
{
    [Header("Equipment")] public int cost;
    public EquipmentSlot slot;
    public bool classRestricted;
    [HiddenIfNot("classRestricted")] public HeroClass reqClass;

    public List<ItemEffect> effects;

    public void OnEquip(Hero hero)
    {
        foreach (var effect in effects)
            effect.OnEquip(hero);
    }

    public void OnUnequip(Hero hero)
    {
        foreach (var effect in effects)
            effect.OnUnequip(hero);
    }
}