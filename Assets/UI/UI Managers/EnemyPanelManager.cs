using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyPanelManager : UnitPanelManager
{
    [Header("Enemy")]
    public Enemy enemy;
    public TextMeshProUGUI enemyName;

    void Start()
    {
        unit = enemy;
        unitImage.sprite = enemy.enemyData.icon;
        enemyName.text = enemy.enemyData.name;
    }
}