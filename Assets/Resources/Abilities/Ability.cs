using System;

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

    internal void NextTurn() => curCooldown = Math.Max(curCooldown--, 0);
}