using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class EffectData : EntityData
    {
        public int Duration { get; set; }
        public Dictionary<string, int> StatsAffected { get; set; }
    }
}