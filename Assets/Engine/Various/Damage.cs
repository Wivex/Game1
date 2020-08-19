using System;
using UnityEngine;

public enum DamageType
{
    Physical,
    Elemental,
    Bleeding
}

internal class Damage
{
    internal int amount;
    internal DamageType type;
    internal Sprite icon;

    public Damage(DamageType type, int amount)
    {
        this.type = type;
        this.amount = amount;

        switch (type)
        {
            case DamageType.Physical:
                icon = UIManager.i.sprites.physicalDamageType;
                break;
            case DamageType.Elemental:
                icon = UIManager.i.sprites.fireDamageType;
                break;
            case DamageType.Bleeding:
                icon = UIManager.i.sprites.bleedingDamageType;
                break;
        }
    }
}