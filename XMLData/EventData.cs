using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class EventData : EntityData
    {
        public float ChanceToOccur { get; set; }
        public int StartAt { get; set; }
        public int MinFrequency { get; set; }
    }
}
