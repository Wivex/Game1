using Game1.Concepts;
using Game1.Objects;
using XMLData;

namespace Game1.Mechanics
{
    public class Ability : Entity
    {
        public override string DataClassPath => "Abilities";

        public int Damage { get; set; }
        public int Cooldown { get; set; }

        public AbilityData XMLData { get; set; }

        public Ability(string xmlDataPath)
        {
            var path = $"{DataClassPath}/{xmlDataPath}";
            XMLData = Globals.TryLoadData<AbilityData>(path);
            Texture = Globals.TryLoadTexture(path);

            Name = XMLData.Name;

            Damage = XMLData.Damage;
            Cooldown = XMLData.Cooldown;
        }
    }
}