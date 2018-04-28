using System.Collections.Generic;
using XMLData.Units;

namespace XMLData
{
    public class ClassData : EntityData
    {
        public Dictionary<string, int> ClassStats { get; } = new Dictionary<string, int>();
        public Dictionary<string, int> ClassAbilities { get; } = new Dictionary<string, int>();
        /// <summary>
        /// (slot, item name)
        /// </summary>
        public Dictionary<string, string> ClassEquipment { get; } = new Dictionary<string, string>();
    }
}