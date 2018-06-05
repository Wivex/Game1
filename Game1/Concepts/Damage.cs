using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Concepts
{
    /// <summary>
    /// Damage source type
    /// </summary>
    public enum DamageType
    {
        /// <summary>
        /// Pure damage, cannot be decreased
        /// </summary>
        Life,
        /// <summary>
        /// can be decreased by Defence
        /// </summary>
        Physical,
        /// <summary>
        /// can be decreased by Resistance
        /// </summary>
        Magic
    }

    public class Damage
    {
        public DamageType DamageType { get; set; }
        public int Value { get; set; }

        public Damage(DamageType type, int value)
        {
            DamageType = type;
            Value = value;
        }
    }
}