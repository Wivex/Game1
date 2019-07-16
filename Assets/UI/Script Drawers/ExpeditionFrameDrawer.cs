using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExpeditionFrameDrawer : MonoBehaviour, IPointerClickHandler
{
    public Image locImage;
    public TextMeshProUGUI expLabel;

    internal LocationData locData;
    internal MonoBehaviour parentDrawer;

    public void Init(LocationData locData, MonoBehaviour parentDrawer)
    {
        this.locData = locData;
        this.parentDrawer = parentDrawer;
        locImage.sprite = locData.icon;
        expLabel.text = $"Explore\nthe {locData.name}";
    }

    // add OnClick event
    public void OnPointerClick(PointerEventData eventData)
    {
        (parentDrawer as MayorPanelDrawer)?.OnExpeditionSelect(this);
    }
}