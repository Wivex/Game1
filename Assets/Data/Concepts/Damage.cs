/// <summary>
/// Damage source type
/// </summary>
public enum DamageType
{
    /// <summary>
    /// Pure damage, cannot be decreased
    /// </summary>
    Physical,
    /// <summary>
    /// can be decreased by Defence
    /// </summary>
    Hazardous,
    /// <summary>
    /// can be decreased by Resistance
    /// </summary>
    Vital
}

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
