using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Items/Equipment Data")]
public class EquipmentData : ItemData
{
    [Header("Equipment")]
    public InventorySlot slot;

    public bool classRestricted;

    [HideIfNot("classRestricted")]
    public ClassType reqClassType;

    public StatModifier[] statModifiers;

    public void Equip(Hero hero)
    {
        //foreach (var mod in statModifiers) hero.baseStats[(int) mod.stat].AddModifier(mod);
    }

    public void Unequip(Hero hero)
    {
        //foreach (var mod in statModifiers)
            //hero.baseStats[(int) mod.stat].RemoveAllModsFromSource(this);
    }   
}