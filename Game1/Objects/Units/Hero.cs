using System.Collections.Generic;
using Game1.Concepts;
using Game1.Mechanics;
using XMLData;

namespace Game1.Objects.Units
{
    public class Hero : Unit
    {
        public static int HeroIndex { get; set; }

        public override string DataClassPath => "Classes";

        public HeroData XMLData { get; set; }

        public int ID { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }

        public Dictionary<Consumable, int> Consumables { get; set; } = new Dictionary<Consumable, int>(10);
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
        public Hero(string xmlDataPath)
        {
            var path = $"{DataClassPath}/{xmlDataPath}";
            XMLData = Globals.TryLoadData<HeroData>(path);
            Texture = Globals.TryLoadTexture(path);

            Name = XMLData.Name;
            ID = HeroIndex++;

            //Health = XMLData.Stats[Stat.Health];
            //Attack = XMLData.Stats[Stat.Attack];
            //Defence = XMLData.Stats[Stat.Defence];
            //Resistance = XMLData.Stats[Stat.Resistance];
            //Speed = XMLData.Stats[Stat.Speed];

            //foreach (var abilityData in XMLData.Abilities)
            //{
            //    Abilities.Add(new Ability(abilityData.Name));
            //}

            // add new hero to global reference
            Globals.HeroesDict.Add(ID, this);
        }

        //public void Equip(Equipment item)
        //{
        //    if (Outfit[item.SlotName] != null)
        //    {
        //        // move equipped item to inventory
        //        Inventory.Add(item, 1);
        //    }

        //    Outfit[item.SlotName] = item;
        //}
    }
}