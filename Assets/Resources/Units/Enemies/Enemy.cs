using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Enemy : Unit
{
    [Header("Enemy")] public EnemyData enemyData;

    public Enemy(EnemyData data)
    {
        enemyData = data;
        name = data.name;
        tacticsPreset = data.tacticsPreset;
        InitAbilities();
        InitStats();
    }

    public override void InitStats()
    {
        baseStats[(int) StatType.Health].BaseValue = enemyData.stats.health;
        baseStats[(int) StatType.Energy].BaseValue = enemyData.stats.mana;
        baseStats[(int) StatType.Attack].BaseValue = enemyData.stats.attack;
        baseStats[(int) StatType.Defence].BaseValue = enemyData.stats.defence;
        baseStats[(int) StatType.Speed].BaseValue = enemyData.stats.speed;
        baseStats[(int) StatType.HResist].BaseValue = enemyData.stats.hazardResistance;
        baseStats[(int) StatType.BResist].BaseValue = enemyData.stats.bleedResistance;
    }

    public override void InitAbilities()
    {
        foreach (var abilityData in enemyData.abilities)
            abilities.Add(new Ability(abilityData));
    }
}