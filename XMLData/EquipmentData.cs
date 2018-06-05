using System.Collections.Generic;

namespace XMLData
{
    public class EquipmentData : ItemData
    {
        public string Slot { get; set; }
        public Dictionary<string, int> Stats { get; set; }
    }
}