using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [Header("Enemy")] public EnemyData enemyData;

    public override void SetStats()
    {
        stats[(int)StatType.Health].maxValue = enemyData.stats.health;
        stats[(int)StatType.Mana].maxValue = enemyData.stats.mana;
        stats[(int)StatType.Attack].maxValue = enemyData.stats.attack;
        stats[(int)StatType.Defence].maxValue = enemyData.stats.defence;
        stats[(int)StatType.Speed].maxValue = enemyData.stats.speed;
        stats[(int)StatType.HResist].maxValue = enemyData.stats.hazardResistance;
        stats[(int)StatType.BResist].maxValue = enemyData.stats.bleedResistance;
    }
}