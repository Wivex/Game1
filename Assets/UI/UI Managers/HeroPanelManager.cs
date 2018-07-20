using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroPanelManager : UnitPanelManager
{
    [Header("Hero")]
    public Hero hero;
    public Image goldImage;
    public TextMeshProUGUI className,gold,exp;
    public Slider expBar;
    public List<Sprite> goldSprites;

    void Start()
    {
        // NOTE: check if safe
        hero = GameManager.expeditions.Last().Key;
        hero.SetStats();
        unit = hero;
        unitImage.sprite = hero.classData.classLevels[hero.level].icon;
        unitName.text = hero.name;
        className.text = hero.classData.classLevels[hero.level].name;
    }

    protected override void Update()
    {
        base.Update();
        
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