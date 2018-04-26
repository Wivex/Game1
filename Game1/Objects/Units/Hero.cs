using System.Collections.Generic;

namespace Game1.Objects.Units
{
    public class Hero : Unit
    {
        public Dictionary<Consumable, int> Consumables { get; } = new Dictionary<Consumable, int>();
        public Dictionary<Item, int> Inventory { get; } = new Dictionary<Item, int>();
        public Dictionary<string, Equipment> Outfit { get; } = new Dictionary<string, Equipment>
        {
            {"Head", null},
            {"Amulet", null},
            {"Body", null},
            {"Belt", null},
            {"Gloves", null},
            {"Boots", null},
            {"HandFirst", null},
            {"HandSecond", null}
        };
        public int Gold { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }

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