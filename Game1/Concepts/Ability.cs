using System.Collections.Generic;
using Game1.Concepts;
using Game1.Engine;
using Game1.Objects;
using XMLData;

namespace Game1.Mechanics
{
    public class Ability : Entity
    {
        public int Cooldown { get; set; }
        public bool Ready => Cooldown == 0;

        public List<Effect> Effects { get; set; } = new List<Effect>();

        public AbilityData XMLData { get; set; }

        public Ability(string abilityName)
        {
            Name = abilityName;
            XMLData = DB.Abilities[abilityName].Item1;
            Texture = DB.Abilities[abilityName].Item2;

            foreach (var effectData in XMLData.Effects)
            {
                Effects.Add(new Effect(effectData));
            }

            Cooldown = XMLData.Cooldown;
        }

        public void Use(Unit actor, Unit target)
        {
            if (XMLData.Damage != 0)
                target.TakeDamage(new Damage(DamageType.Magic, XMLData.Damage),Texture);
            TryApplyEffects(actor, target);
            CDReset();
        }

        public void CDReset()
        {
            Cooldown = DB.Abilities[Name].Item1.Cooldown;
        }

        public void TryApplyEffects(Unit actor, Unit target)
        {
            foreach (var effect in Effects)
            {
                if (effect.XMLData.TargetSelf)
                    actor.Effects.Add(effect);
                else
                    target.Effects.Add(effect);
            }
        }
    }
}