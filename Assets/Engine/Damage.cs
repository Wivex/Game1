public class Damage
{
    public DamageType DamageType { get; set; }
    public int Value { get; set; }

    public Damage(DamageType type, int value)
    {
        DamageType = type;
        Value = value;
    }
}
