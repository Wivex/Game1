using System;

[Serializable]
public class Ability
{
    internal AbilityData data;
    internal int curCooldown;

    internal Ability(AbilityData data)
    {
        this.data = data;
    }

    internal bool Ready(Unit unit) =>
        curCooldown <= 0 && unit.Energy >= data.energyCost;
}