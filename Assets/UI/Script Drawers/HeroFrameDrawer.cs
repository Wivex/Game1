﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroFrameDrawer : MonoBehaviour, IPointerClickHandler
{
    public Image heroImage;
    public TextMeshProUGUI heroNameText, heroLevelText;

    internal Hero hero;
    internal MonoBehaviour parentDrawer;

    public void Init(Hero hero, MonoBehaviour parentDrawer)
    {
        this.hero = hero;
        this.parentDrawer = parentDrawer;
        heroImage.sprite = hero.portrait;
        heroNameText.text = hero.name;
        heroLevelText.text = hero.level.ToString();
    }

    // add OnClick event
    public void OnPointerClick(PointerEventData eventData)
    {
        (parentDrawer as MayorPanelDrawer)?.OnHeroSelect(this);
        (parentDrawer as TavernPanelDrawer)?.OnHeroSelect(this);
    }
}