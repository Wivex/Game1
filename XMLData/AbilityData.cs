using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLData.Units;

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