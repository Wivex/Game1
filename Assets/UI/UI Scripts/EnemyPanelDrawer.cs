﻿using UnityEngine;
using TMPro;

public class EnemyPanelDrawer : UnitPanelDrawer
{
    [Header("Enemy")] public Enemy enemy;
    public TextMeshProUGUI enemyName;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        unit = enemy;
        unitImage.sprite = enemy.enemyData.icon;
        enemyName.text = enemy.enemyData.name;
    }

    protected override void Update()
    {
        if (!enemy.spawned) return;

        base.Update();
    }
}