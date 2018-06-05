using System.Collections.Generic;

namespace XMLData
{
    public class EnemyData : UnitData
    {
        public int XPReward { get; set; }

        /// <summary>
        /// 
        /// (ItemName, DropChance)
        /// </summary>
        public Dictionary<string, float> DropTable { get; set; }
    }
}