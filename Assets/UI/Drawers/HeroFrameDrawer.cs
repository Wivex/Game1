using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroFrameDrawer : Drawer
{
    public Image heroImage;
    public TextMeshProUGUI heroNameText, heroLevelText;

    internal Hero hero;
    internal Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Init(Hero hero)
    {
        this.hero = hero;
        heroImage.sprite = hero.portrait;
        heroNameText.text = hero.name;
        heroLevelText.text = $"Level {hero.level} {hero.heroClassType}";
    }
}