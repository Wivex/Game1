using UnityEngine;

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
    MainHand,
    OffHand
}

[CreateAssetMenu(menuName = "Content/Data/Items/Equipment Data")]
public class EquipmentData : ItemData
{
    [Header("Equipment")] public int cost;
    public InventorySlot slot;
    public bool classRestricted;
    [HiddenIfNot("classRestricted")] public HeroClass reqClass;

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