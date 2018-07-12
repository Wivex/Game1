using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroPanelManager : MonoBehaviour
{
    public Hero hero;
    public Image heroImage;
    public TextMeshProUGUI heroName, className, attack,defence,speed,hResist,bResist,gold,health,mana,init,exp;
    public Slider healthBar, manaBar, initBar, expBar;

    HeroPanelManager(Hero hero)
    {
        this.hero = hero;
    }

    void Start()
    {
        heroImage.sprite = hero.heroClass.icon;
        heroName.text = hero.name;
        className.text = hero.heroClass.name;
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

        //gold.text = hero.gold.ToString();
    }
}