using System.Collections.Generic;
using Game1.Mechanics;

namespace Game1.Objects
{
    public abstract class Unit : Entity
    {
        public int Health { get; set; } = 100;
        public int Attack { get; set; } = 1;
        public int Defence { get; set; } = 0;
        public int Resistance { get; set; } = 0;
        /// <summary>
        /// Affect first turn order and amount of action points per GameTick
        /// </summary>
        public int Speed { get; set; } = 10;

        /// <summary>
        /// Normal amount of action points for unit to take action. Modified by recovery penalties and other factors
        /// </summary>
        public int ActionCost { get; set; } = 100;

        /// <summary>
        /// Current amount of action points. Incremented each GameTick
        /// </summary>
        public double ActionPoints { get; set; } = 0;

        public List<Ability> Abilities { get; set; } = new List<Ability>();
    }
}