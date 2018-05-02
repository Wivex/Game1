using System.Collections.Generic;

namespace XMLData
{
    public class AbilityData : EntityData
    {
        public int Damage { get; }
        public int DoT { get; }
        public int Duration { get; }
        public int Cooldown { get; }
        /// <summary>
        /// used to counter enemy attack or ability
        /// </summary>
        public bool Counter { get; }
        public Dictionary<string, int> SelfStatEffects { get; } = new Dictionary<string, int>();
        public Dictionary<string, int> EnemyStatEffects { get; } = new Dictionary<string, int>();
    }
}