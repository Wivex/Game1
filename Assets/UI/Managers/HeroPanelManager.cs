using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroPanelManager : MonoBehaviour
{
    public Hero hero;
    public Image heroImage, goldImage;
    public TextMeshProUGUI heroName, className, attack,defence,speed,hResist,bResist,gold,health,mana,init,exp;
    public Slider healthBar, manaBar, initBar, expBar;
    public List<Sprite> goldSprites;

    HeroPanelManager(Hero hero)
    {
        this.hero = hero;
    }

    void Start()
    {
        heroImage.sprite = hero.classData.classLevels[hero.level-1].icon;
        heroName.text = hero.name;
        className.text = hero.classData.classLevels[hero.level - 1].name;
    }

    void Update()
    {
        attack.text = $"A: {hero.stats[(int)StatType.Attack].maxValue}";
        defence.text = $"D: {hero.stats[(int)StatType.Defence].maxValue}";
        speed.text = $"S: {hero.stats[(int)StatType.Speed].maxValue}";
        hResist.text = $"HR: {hero.stats[(int)StatType.HResist].maxValue}";
        bResist.text = $"BR: {hero.stats[(int)StatType.BResist].maxValue}";

        healthBar.value = (float)hero.stats[(int)StatType.Health].curValue / hero.stats[(int)StatType.Health].maxValue;
        health.text = $"{hero.stats[(int)StatType.Health].curValue} / {hero.stats[(int)StatType.Health].maxValue}";
        manaBar.value = (float)hero.stats[(int)StatType.Mana].curValue / hero.stats[(int)StatType.Mana].maxValue;
        mana.text = $"{hero.stats[(int)StatType.Mana].curValue} / {hero.stats[(int)StatType.Mana].maxValue}";

        gold.text = hero.gold.ToString();
        //PERF: check performance?
        var a = 0;
        var i = 0;
        while (hero.gold > a)
        {
            a = (int)Mathf.Pow(2, i++);
            if (i >= goldSprites.Count-1) break;
        }
        goldImage.sprite = goldSprites[i];
    }
}