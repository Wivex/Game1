using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionFrameDrawer : Drawer, IPointerClickHandler
{
    public Image locImage;
    public TextMeshProUGUI expLabel;

    internal ZoneData locData;
    internal MonoBehaviour parentDrawer;

    public void Init(ZoneData locData, MonoBehaviour parentDrawer)
    {
        this.locData = locData;
        this.parentDrawer = parentDrawer;
        locImage.sprite = locData.sites[0].siteChainSprites.First();
        expLabel.text = $"Explore\nthe {locData.name}";
    }

    // add OnClick event
    public void OnPointerClick(PointerEventData eventData)
    {
        (parentDrawer as MayorPanelDrawer)?.OnMissionSelect(this);
    }
}