using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace XMLData
{
    public class ClassData : EntityData
    {
        /// <summary>
        /// Base hero stats of this class
        /// </summary>
        public Dictionary<string, int> ClassStats { get; } = new Dictionary<string, int>();

        /// <summary>
        /// (AbilityName, LvlRequirement)
        /// </summary>
        public Dictionary<string, int> ClassAbilities { get; } = new Dictionary<string, int>();

        /// <summary>
        /// Class starting equipment
        /// (SlotName, EquipmentName)
        /// </summary>
        public Dictionary<string, string> ClassEquipment { get; } = new Dictionary<string, string>();
    }
}