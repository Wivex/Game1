using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class EquipmentData : ItemData
    {
        public string SlotName { get; set; }
        public Dictionary<string, int> Stats { get; set; } = new Dictionary<string, int>();
    }
}