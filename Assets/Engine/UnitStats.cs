[System.Serializable]
public struct UnitStats
{
    public Stat health;
    public Stat mana;
    public Stat attack;
    public Stat speed;
    public Stat defence;
    public Stat hazardResistance;
    public Stat bleedResistance;

    //public static bool operator ==(UnitStats a, UnitStats b)
    //{
    //    return a.health == b.health &&
    //           a.mana == b.mana &&
    //           a.attack == b.attack &&
    //           a.speed == b.speed &&
    //           a.defence == b.defence &&
    //           a.hazardResistance == b.hazardResistance &&
    //           a.bleedResistance == b.bleedResistance;
    //}

    //public static bool operator !=(UnitStats a, UnitStats b)
    //{
    //    return a.health != b.health ||
    //           a.mana != b.mana ||
    //           a.attack != b.attack ||
    //           a.speed != b.speed ||
    //           a.defence != b.defence ||
    //           a.hazardResistance != b.hazardResistance ||
    //           a.bleedResistance != b.bleedResistance;
    }
}