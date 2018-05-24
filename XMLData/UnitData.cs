using System.Collections.Generic;

namespace XMLData
{
    public abstract class UnitData : EntityData
    {
        /// <summary>
        /// Base stats for this enemy type
        /// </summary>
        public Dictionary<string, int> Stats { get; set; } = new Dictionary<string, int>();

        public List<AbilityData> Abilities { get; set; } = new List<AbilityData>();
    }
}