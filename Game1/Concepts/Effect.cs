using System.Collections.Generic;
using Game1.Objects;

namespace Game1.Concepts
{
    public class Effect : Entity
    {
        public int Duration { get; set; }
        public Dictionary<string, int> StatsAffected { get; set; }
    }
}