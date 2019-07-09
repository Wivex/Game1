using System.Collections;
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

    public void Init(Hero hero)
    {
        this.hero = hero;
        heroImage.sprite = hero.portrait;
        heroNameText.text = hero.name;
        heroLevelText.text = hero.level.ToString();
    }

    // add OnClick event
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.mayorPanelDrawer.OnHeroSelect(this);
    }
}