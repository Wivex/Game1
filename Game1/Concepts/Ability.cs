using Game1.Engine;
using Game1.Objects;

namespace Game1.Mechanics
{
    public class Ability : Entity
    {
        public int Damage { get; set; }
        public int Cooldown { get; set; }

        public Ability(string abilityName)
        {
            Name = abilityName;
            var XMLData = DataBase.Abilities[abilityName].Item1;
            Texture = DataBase.Abilities[abilityName].Item2;

            Damage = XMLData.Damage;
            Cooldown = XMLData.Cooldown;
        }
    }
}