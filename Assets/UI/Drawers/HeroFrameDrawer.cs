using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroFrameDrawer : Drawer, IPointerClickHandler
{
    public Image heroImage;
    public TextMeshProUGUI heroNameText, heroLevelText;

    internal Hero hero;

    MonoBehaviour parentDrawer;

    public void Init(Hero hero, MonoBehaviour parentDrawer)
    {
        this.hero = hero;
        this.parentDrawer = parentDrawer;
        heroImage.sprite = hero.portrait;
        heroNameText.text = hero.name;
        heroLevelText.text = $"Level {hero.level} {hero.heroClassType.ToString()}";
    }

    // add OnClick event to the OnClick event list of button component
    public void OnPointerClick(PointerEventData eventData)
    {
        (parentDrawer as MayorPanelDrawer)?.OnHeroSelect(this);
        (parentDrawer as TavernPanelDrawer)?.OnHeroSelect(this);
    }
}