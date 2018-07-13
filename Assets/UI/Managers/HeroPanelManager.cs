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
        heroImage.sprite = hero.classData.icon;
        heroName.text = hero.name;
        className.text = hero.classData.name;
    }

    void Update()
    {
        //attack.text = $"A: {hero.curStats.attack.Value}";
        //defence.text = $"D: {hero.stats.defence.Value}";
        //speed.text = $"S: {hero.stats.speed.Value}";
        //hResist.text = $"HR: {hero.stats.hazardResistance.Value}";
        //bResist.text = $"BR: {hero.stats.bleedResistance.Value}";

        //healthBar.value = hero.stats.health.Value;
        //health.text = $"{hero.stats.health.Value} / {hero.stats.health.Value}";

        gold.text = hero.gold.ToString();

        //PERF: check?
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