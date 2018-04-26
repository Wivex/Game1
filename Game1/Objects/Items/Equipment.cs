using System.Collections.Generic;
using Game1.Concepts;

namespace Game1.Objects
{
    public class Equipment : Item
    {
        public string SlotName { get; set; }

        public Dictionary<string, Stat> Stats { get; } = new Dictionary<string, Stat>();
    }
}