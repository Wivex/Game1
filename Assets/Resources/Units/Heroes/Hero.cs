using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public enum HeroClass
{
    Warrior,
    Mage,
    Rogue
}

[Serializable]
public class Hero : Unit
{
    [Header("Hero")] public ClassData classData;

    public string name;
    public int level = 1;
    public int gold;

    public EquipmentData[] equipment = new EquipmentData[Enum.GetNames(typeof(EquipmentSlot)).Length];

    public Hero(string name)
    {
        this.name = name;
        classData = Resources.Load<ClassData>("Units/Heroes/Classes/Warrior/WarriorClass");
        SetStats();
    }

    public override UnitPanelManager Panel
    {
        get
        {
            var expedition = ExpeditionManager.expeditions[this];
            return expedition.expeditionPanel.heroPanel;
        }
    }

    public override void SetStats()
    {
        stats[(int) StatType.Health].BaseValue = classData.classLevels[level - 1].stats.health;
        stats[(int) StatType.Mana].BaseValue = classData.classLevels[level - 1].stats.mana;
        stats[(int) StatType.Attack].BaseValue = classData.classLevels[level - 1].stats.attack;
        stats[(int) StatType.Defence].BaseValue = classData.classLevels[level - 1].stats.defence;
        stats[(int) StatType.Speed].BaseValue = classData.classLevels[level - 1].stats.speed;
        stats[(int) StatType.HResist].BaseValue = classData.classLevels[level - 1].stats.hazardResistance;
        stats[(int) StatType.BResist].BaseValue = classData.classLevels[level - 1].stats.bleedResistance;

        stats[(int) StatType.Health].curValue = stats[(int) StatType.Health].BaseValue;
        stats[(int) StatType.Mana].curValue = stats[(int) StatType.Mana].BaseValue;
        stats[(int) StatType.Attack].curValue = stats[(int) StatType.Attack].BaseValue;
        stats[(int) StatType.Defence].curValue = stats[(int) StatType.Defence].BaseValue;
        stats[(int) StatType.Speed].curValue = stats[(int) StatType.Speed].BaseValue;
        stats[(int) StatType.HResist].curValue = stats[(int) StatType.HResist].BaseValue;
        stats[(int) StatType.BResist].curValue = stats[(int) StatType.BResist].BaseValue;
    }

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