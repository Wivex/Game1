using System;
using System.Collections.Generic;
using Game1.Concepts;
using Game1.Engine;
using Game1.Mechanics;
using Game1.Objects.Units;
using Game1.UI;
using Microsoft.Xna.Framework.Graphics;
using Effect = Game1.Concepts.Effect;

namespace Game1.Objects
{
    public abstract class Unit : Entity
    {
        /// <summary>
        /// Normal amount of action points for unit to take action. Modified by recovery penalties and other factors
        /// </summary>
        public int ActionCost { get; set; } = 100;
        /// <summary>
        /// Current amount of action points. Incremented each GameTick
        /// </summary>
        public double ActionPoints { get; set; } = 0;

        public Dictionary<string, Stat> Stats { get; set; } = new Dictionary<string, Stat>();

        public List<Ability> Abilities { get; set; } = new List<Ability>();

        public List<Effect> Effects { get; set; } = new List<Effect>();

        public PanelEmpty UnitPanel { get; set; }

        public void UpdateEffects()
        {
            for (var i = 0; i < Effects.Count; i++)
            {
                var effect = Effects[i];
                if (effect.Duration > 0)
                {
                    effect.Duration--;
                }
                else
                    Effects.Remove(effect);
            }
        }

        public virtual void TakeDamage(Damage damage, Texture2D icon = null)
        {
            Stats[Stat.Health].Value -= damage.Value;
            var damText = damage.Value > 0 ? $"-{damage.Value}" : $"+{damage.Value}";
            UnitPanel.InitFloatingText(damText, TimeSpan.FromSeconds(2), icon);
        }
    }
}