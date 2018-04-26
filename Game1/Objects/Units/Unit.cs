using System.Collections.Generic;
using Game1.Concepts;

namespace Game1.Objects
{
    public abstract class Unit : Entity
    {
        public Dictionary<string, Stat> Stats { get; } = new Dictionary<string, Stat>
        {
            {"Health", new Stat("Health", 100)},
            {"Attack", new Stat("Attack", 10)},
            {"Defence", new Stat("Defence", 5)},
            {"Speed", new Stat("Speed", 3)}
        };
    }
}