using System.Collections.Generic;

namespace XMLData
{
    public abstract class UnitData : EntityData
    {
        /// <summary>
        /// Base stats for this enemy type
        /// </summary>
        public Dictionary<string, int> Stats { get; set; }

        public List<string> Abilities { get; set; }
    }
}