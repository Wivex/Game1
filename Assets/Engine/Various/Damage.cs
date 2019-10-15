using System;

public enum DamageType
{
    Physical,
    Elemental,
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
}