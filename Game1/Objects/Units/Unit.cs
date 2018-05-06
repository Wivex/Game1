using System.Collections.Generic;

namespace Game1.Objects
{
    public abstract class Unit : Entity
    {
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Resistance { get; set; }
        public int Speed { get; set; }

        public Dictionary<string, int> BaseStats { get; set; } = new Dictionary<string, int>();
    }
}