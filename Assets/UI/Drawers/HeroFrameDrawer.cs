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
    internal Button button;
    
    // TODO: remove
    MonoBehaviour parentDrawer;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Init(Hero hero, MonoBehaviour parentDrawer)
    {
        this.hero = hero;
        this.parentDrawer = parentDrawer;
        heroImage.sprite = hero.portrait;
        heroNameText.text = hero.name;
        heroLevelText.text = $"Level {hero.level} {hero.heroClassType}";
    }
    public void Init(Hero hero)
    {
        this.hero = hero;
        heroImage.sprite = hero.portrait;
        heroNameText.text = hero.name;
        heroLevelText.text = $"Level {hero.level} {hero.heroClassType}";
    }

    // TODO: remove, use listeners instead
    // add OnClick event to the OnClick event list of button component (can't select methods with parameters in button editor)
    public void OnPointerClick(PointerEventData eventData)
    {
        //(parentDrawer as HeroSelectionPanelDrawer)?.OnHeroSelect(this);
        (parentDrawer as MayorPanelDrawer)?.OnHeroSelect(this);
        (parentDrawer as TavernPanelDrawer)?.OnHeroSelect(this);
    }
}