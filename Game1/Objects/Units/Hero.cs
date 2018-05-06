using System.Collections.Generic;
using Game1.Concepts;
using Microsoft.Xna.Framework.Graphics;
using XMLData;

namespace Game1.Objects.Units
{
    public class Hero : Unit
    {
        public static int UniqueID { get; set; }

        public int ID { get; set; }
        public ClassData ClassData { get; set; }
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
            {Slot.Mainhand, null},
            {Slot.Offhand, null}
        };

        //TODO: check if Content.Load accesses HDD (load XMLData to globals instead then)
        public Hero(string name, string className)
        {
            ClassData = Globals.Game.Content.Load<ClassData>(@"Settings/Classes/" + className);

            ID = UniqueID++;
            Name = name;
            Texture = Globals.TryLoadTexture(@"Textures/Classes/", ClassData.TextureName);
            BaseStats = new Dictionary<string, int>(ClassData.ClassStats);

            Health = BaseStats[Stat.Health];

            // add new hero to global reference
            Globals.HeroesDict.Add(ID,this);
        }

        public void Equip(Equipment item)
        {
            if (Outfit[item.SlotName] != null)
            {
                // move equipped item to inventory
                Inventory.Add(item, 1);
            }

            Outfit[item.SlotName] = item;
        }
    }
}