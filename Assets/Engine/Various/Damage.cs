using System;

public enum DamageType
{
    Physical,
    Hazardous,
    Bleeding
}

[Serializable]
public class Damage
{
    public DamageType type;
    public int amount;

    public Damage(DamageType type, int amount)
    {
        this.type = type;
        this.amount = amount;
    }

    public Damage(DamageType type, int amount, Unit target)
    {
        this.type = type;
        this.amount = CalculateDamage(type, amount, target);
    }

    int CalculateDamage(DamageType type, int amount, Unit target)
    {
        switch (type)
        {
            case DamageType.Physical:
                return Math.Max(amount - target.stats[(int) StatType.Defence].curValue, 0);
            case DamageType.Hazardous:
                return Math.Max(amount - target.stats[(int) StatType.HResist].curValue, 0);
            case DamageType.Bleeding:
                return Math.Max(amount - target.stats[(int) StatType.BResist].curValue, 0);
            default:
                throw new ArgumentException();
        }
    }
}