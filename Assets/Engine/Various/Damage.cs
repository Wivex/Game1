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
        var protectionValue = 0;
        switch (type)
        {
            case DamageType.Physical:
                protectionValue = target.stats[(int) StatType.Defence].curValue;
                break;
            case DamageType.Hazardous:
                protectionValue = target.stats[(int) StatType.HResist].curValue;
                break;
            case DamageType.Bleeding:
                protectionValue = target.stats[(int) StatType.BResist].curValue;
                break;
        }
        return Math.Max(amount - protectionValue, 0);
    }
}