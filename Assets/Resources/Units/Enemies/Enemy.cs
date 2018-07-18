﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class Enemy : Unit
{
    EnemyPanelManager enemyPanel;
    [Header("Enemy")] public EnemyData enemyData;

    public Enemy(EnemyData data)
    {
        enemyData = data;
        SetStats();
    }

    public override UnitPanelManager Panel
    {
        get
        {
            var expedition = ExpeditionManager.expeditions.Values.FirstOrDefault(exp =>
                exp.situation.type == SituationType.EnemyEncounter &&
                (exp.situation as SituationCombat)?.enemy == this);
            return expedition?.expeditionPanel.enemyPanel;
        }
    }

    public override void SetStats()
    {
        stats[(int) StatType.Health].BaseValue = enemyData.stats.health;
        stats[(int) StatType.Mana].BaseValue = enemyData.stats.mana;
        stats[(int) StatType.Attack].BaseValue = enemyData.stats.attack;
        stats[(int) StatType.Defence].BaseValue = enemyData.stats.defence;
        stats[(int) StatType.Speed].BaseValue = enemyData.stats.speed;
        stats[(int) StatType.HResist].BaseValue = enemyData.stats.hazardResistance;
        stats[(int) StatType.BResist].BaseValue = enemyData.stats.bleedResistance;
    }
}