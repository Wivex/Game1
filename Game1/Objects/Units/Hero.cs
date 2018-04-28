using System.Collections.Generic;
using Game1.Concepts;

namespace Game1.Objects.Units
{
    public class Hero : Unit
    {
        public Dictionary<Consumable, int> Consumables { get; } = new Dictionary<Consumable, int>(10);
        public Dictionary<Item, int> Inventory { get; } = new Dictionary<Item, int>();
        public Dictionary<string, Equipment> Outfit { get; } = new Dictionary<string, Equipment>
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