using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyPanelManager : MonoBehaviour
{
    public Enemy enemy;
    public Image enemyImage;

    public TextMeshProUGUI enemyName,
        attack,
        defence,
        speed,
        hResist,
        bResist,
        health,
        mana,
        init;

    public Slider healthBar, manaBar, initBar;

    EnemyPanelManager(Enemy enemy)
    {
        this.enemy = enemy;
    }

    void Start()
    {
        enemyImage.sprite = enemy.enemyData.icon;
        enemyName.text = enemy.enemyData.name;
    }

    void Update()
    {
        attack.text = $"A: {enemy.stats[(int) StatType.Attack].maxValue}";
        defence.text = $"D: {enemy.stats[(int) StatType.Defence].maxValue}";
        speed.text = $"S: {enemy.stats[(int) StatType.Speed].maxValue}";
        hResist.text = $"HR: {enemy.stats[(int) StatType.HResist].maxValue}";
        bResist.text = $"BR: {enemy.stats[(int) StatType.BResist].maxValue}";

        healthBar.value = (float) enemy.stats[(int) StatType.Health].curValue /
                          enemy.stats[(int) StatType.Health].maxValue;
        health.text = $"{enemy.stats[(int) StatType.Health].curValue} / {enemy.stats[(int) StatType.Health].maxValue}";
        manaBar.value = (float) enemy.stats[(int) StatType.Mana].curValue / enemy.stats[(int) StatType.Mana].maxValue;
        mana.text = $"{enemy.stats[(int) StatType.Mana].curValue} / {enemy.stats[(int) StatType.Mana].maxValue}";
    }
}