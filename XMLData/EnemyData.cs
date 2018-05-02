using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class EnemyData : EntityData
    {
        public int XPReward { get; set; } = 1;

        /// <summary>
        /// Base stats for this enemy type
        /// </summary>
        public Dictionary<string, int> Stats { get; } = new Dictionary<string, int>();

        /// <summary>
        /// (AbilityName, Cooldown)
        /// </summary>
        public Dictionary<string, int> Abilities { get; } = new Dictionary<string, int>();

        /// <summary>
        /// 
        /// (ItemName, DropChance)
        /// </summary>
        public Dictionary<string, float> DropTable { get; } = new Dictionary<string, float>();
    }
}