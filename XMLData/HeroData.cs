using System.Collections.Generic;

namespace XMLData
{
    public class HeroData : UnitData
    {
        /// <summary>
        /// Class starting equipment
        /// (SlotName, EquipmentName)
        /// </summary>
        public Dictionary<string, string> Equipment { get; set; }
    }
}