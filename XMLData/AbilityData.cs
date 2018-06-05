using System.Collections.Generic;

namespace XMLData
{
    public class AbilityData : EntityData
    {
        public int Damage { get; set; }
        public int Cooldown { get; set; }
        public bool IsCounter { get; set; }

        public List<EffectData> Effects { get; set; }
    }
}