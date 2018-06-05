using System.Collections.Generic;
using Game1.Concepts;
using Game1.Engine;
using Game1.Mechanics;
using Game1.UI;
using XMLData;

namespace Game1.Objects.Units
{
    public class Hero : Unit
    {
        public static int HeroIndex { get; set; }

        public HeroData XMLData { get; set; }

        public int ID { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }

        public List<Consumable> Consumables { get; set; } = new List<Consumable>(10);
        public Dictionary<Item, int> Inventory { get; set; } = new Dictionary<Item, int>();
        public Dictionary<string, Equipment> Outfit { get; set; } = new Dictionary<string, Equipment>
        {
            {Slot.Head, null},
            {Slot.Amulet, null},
            {Slot.Body, null},
            {Slot.Belt, null},
            {Slot.Gloves, null},
            {Slot.Boots, null},
            {Slot.Ring1, null},
            {Slot.Ring2, null},
            {Slot.MainHand, null},
            {Slot.OffHand, null}
        };

        //TODO: check if Content.Load accesses HDD (load XMLData to globals instead then)
        public Hero(string heroName, string className)
        {
            ID = HeroIndex++;

            Name = heroName;
            XMLData = DB.Heroes[className].Item1;
            Texture = DB.Heroes[className].Item2;

            foreach (var statData in XMLData.Stats)
                Stats.Add(statData.Key, new Stat(statData.Key, statData.Value));

            foreach (var abilityName in XMLData.Abilities)
                Abilities.Add(new Ability(abilityName));

            // add new hero to global reference
            Globals.HeroesDict.Add(ID, this);
        }
    }
}