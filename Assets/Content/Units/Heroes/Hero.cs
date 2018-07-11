using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum HeroClass
{
    Warrior,
    Mage,
    Rogue
}

public class Hero : Unit
{
    //public EquipmentCollection Outfit = new EquipmentCollection();

    //public void CalculateBaseStats()
    //{
    //    maxStats = DataBase.GetUnitBaseStats(unitClass, level);
    //    foreach (var equip in Outfit)
    //    {
    //        foreach (var effect in equip.effects)
    //        {
    //            effect.OnEquip(ref maxStats);
    //        }
    //    }
    //}
    //    public int curExperience;
    //    public int reqExperience;
    //    public int level;
    //    public int gold;

    //    public Dictionary<Consumable, int> belt = new Dictionary<Consumable, int>(4);
    //    public List<Item> inventory = new List<Item>();



    //[System.Serializable]
    //public class EquipmentCollection : IEnumerable<Equipment>
    //{
    //    [System.Serializable]
    //    private struct SlotWithEquipment
    //    {
    //        public Slot slot;
    //        public Equipment equipment;
    //    }
    //    [SerializeField]
    //    private List<SlotWithEquipment> slots = new List<SlotWithEquipment>();

    //    public Equipment this[Slot slot]
    //    {
    //        get { return slots.FirstOrDefault(s => s.slot == slot).equipment; }
    //        set
    //        {
    //            var index = -1;
    //            for (int i = 0; i < slots.Count; i++)
    //                if (slots[i].slot == slot)
    //                    index = i;
    //            if (index < 0)
    //                slots.Add(new SlotWithEquipment() { slot = slot, equipment = value });
    //            else
    //                slots[index] = new SlotWithEquipment() { slot = slot, equipment = value };
    //        }
    //    }

    //    public IEnumerator<Equipment> GetEnumerator()
    //    {
    //        foreach (var s in slots)
    //            yield return s.equipment;
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        foreach (var s in slots)
    //            yield return s.equipment;
    //    }
    //}
}
