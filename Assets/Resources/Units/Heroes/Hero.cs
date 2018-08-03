using System;
using System.Collections.Generic;
using UnityEngine;

public enum HeroClass
{
    Warrior,
    Mage,
    Rogue
}

[Serializable]
public class Hero : Unit
{
    [Header("Hero")]
    public ClassData classData;

    public int level;
    public int gold, experience;

    public EquipmentData[] inventory = new EquipmentData[Enum.GetNames(typeof(InventorySlot)).Length];
    public List<ItemData> backpack = new List<ItemData>();
    public List<Consumable> consumables = new List<Consumable>();

    public Hero(string name)
    {
        this.name = name;
        classData = Resources.Load<ClassData>("Units/Heroes/Classes/Warrior/WarriorClass");
        tacticsPreset = classData.classLevels[level].tacticsPreset;
        SetAbilities();
        SetStats();
    }

    public override UnitPanelDrawer unitPanel
    {
        get
        {
            var expedition = GameManager.expeditions[this];
            return expedition.expPanel.situationPanel.heroPanel;
        }
    }

    public override void SetStats()
    {
        stats[(int) StatType.Health].BaseValue = classData.classLevels[level].stats.health;
        stats[(int) StatType.Mana].BaseValue = classData.classLevels[level].stats.mana;
        stats[(int) StatType.Attack].BaseValue = classData.classLevels[level].stats.attack;
        stats[(int) StatType.Defence].BaseValue = classData.classLevels[level].stats.defence;
        stats[(int) StatType.Speed].BaseValue = classData.classLevels[level].stats.speed;
        stats[(int) StatType.HResist].BaseValue = classData.classLevels[level].stats.hazardResistance;
        stats[(int) StatType.BResist].BaseValue = classData.classLevels[level].stats.bleedResistance;

        stats[(int) StatType.Health].curValue = stats[(int) StatType.Health].BaseValue;
        stats[(int) StatType.Mana].curValue = stats[(int) StatType.Mana].BaseValue;
        stats[(int) StatType.Attack].curValue = stats[(int) StatType.Attack].BaseValue;
        stats[(int) StatType.Defence].curValue = stats[(int) StatType.Defence].BaseValue;
        stats[(int) StatType.Speed].curValue = stats[(int) StatType.Speed].BaseValue;
        stats[(int) StatType.HResist].curValue = stats[(int) StatType.HResist].BaseValue;
        stats[(int) StatType.BResist].curValue = stats[(int) StatType.BResist].BaseValue;
    }

    public override void SetAbilities()
    {
        foreach (var abilityData in classData.classLevels[level].abilities)
            abilities.Add(new Ability(abilityData));
    }
}