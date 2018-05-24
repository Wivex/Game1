using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace XMLData
{
    public class AbilityData : EntityData
    {
        public int Damage { get; set; }
        public int Cooldown { get; set; }
        /// <summary>
        /// used to counter enemy attack or ability
        /// </summary>
        //public bool IsCounter { get; }
        //public Dictionary<string, int> SelfEffects { get; } = new Dictionary<string, int>();
        //public Dictionary<string, int> EnemyEffects { get; } = new Dictionary<string, int>();
    }
}