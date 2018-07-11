using UnityEngine;

public enum EffectType
{
    Instance,
    Permanent,
    Periodic
}

public abstract class ItemEffect : ScriptableObject
{
    public Stat Stat;
    public int statChange;

    public abstract void OnEquip(Hero hero);
    public abstract void OnUnequip(Hero hero);
}