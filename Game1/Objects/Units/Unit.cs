using System.Collections.Generic;

namespace Game1.Objects
{
    public abstract class Unit : Entity
    {
        public Dictionary<string, int> Stats { get; set; } = new Dictionary<string, int>();
        public int Health { get; set; }
    }
}