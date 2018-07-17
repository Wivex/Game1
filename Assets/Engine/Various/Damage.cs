public enum DamageType
{
    Physical,
    Hazardous,
    Bleeding
}

public enum Target
{
    Self,
    Other
}

public class Damage
{
    public DamageType DamageType;
    public int Value;

    public Damage(DamageType type, int value)
    {
        DamageType = type;
        Value = value;
    }
}

