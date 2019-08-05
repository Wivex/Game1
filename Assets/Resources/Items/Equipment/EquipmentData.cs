﻿using UnityEngine;

public enum InventorySlot
{
    Head,
    Body,
    Arms,
    Boots,
    Amulet,
    Belt,
    Ring1,
    Ring2,
    MainHand1,
    OffHand1,
    MainHand2,
    OffHand2
}

[CreateAssetMenu(menuName = "Content/Data/Items/Equipment Data")]
public class EquipmentData : ItemData
{
    [Header("Equipment")]
    public InventorySlot slot;

    public bool classRestricted;

    [HideIfNot("classRestricted")]
    public HeroClass reqClass;

    public StatModifier[] statModifiers;

    public void Equip(Hero hero)
    {
        foreach (var mod in statModifiers) hero.stats[(int) mod.stat].AddModifier(mod);
    }

    public void Unequip(Hero hero)
    {
        foreach (var mod in statModifiers)
            hero.stats[(int) mod.stat].RemoveAllModsFromSource(this);
    }
}