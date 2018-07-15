using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [Header("Enemy")] public EnemyData enemyData;

    public override void SetStats()
    {
        stats[(int)StatType.Health].BaseValue = enemyData.stats.health;
        stats[(int)StatType.Mana].BaseValue = enemyData.stats.mana;
        stats[(int)StatType.Attack].BaseValue = enemyData.stats.attack;
        stats[(int)StatType.Defence].BaseValue = enemyData.stats.defence;
        stats[(int)StatType.Speed].BaseValue = enemyData.stats.speed;
        stats[(int)StatType.HResist].BaseValue = enemyData.stats.hazardResistance;
        stats[(int)StatType.BResist].BaseValue = enemyData.stats.bleedResistance;
    }
}