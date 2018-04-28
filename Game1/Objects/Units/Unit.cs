using System.Collections.Generic;
using Game1.Concepts;
using Game1.Objects.Units;

namespace Game1.Objects
{
    public abstract class Unit : Entity
    {
        public Dictionary<string, int> Stats { get; } = new Dictionary<string, int>()
        {
            {Stat.Health, 100},
            {Stat.Attack, 10},
            {Stat.Defence, 0},
            {Stat.Speed, 10}
        };
    }
}