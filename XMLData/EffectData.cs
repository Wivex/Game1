using System.Collections.Generic;

namespace XMLData
{
    public class EffectData : EntityData
    {
        public int Duration { get; set; }
        public Dictionary<string, int> StatsAffected { get; set; }
    }
}