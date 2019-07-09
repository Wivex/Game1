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

    public void Init(LocationData locData)
    {
        this.locData = locData;
        locImage.sprite = locData.icon;
        expLabel.text = $"Explore\nthe {locData.name}";
    }

    // add OnClick event
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.mayorPanelDrawer.OnExpeditionSelect(this);
    }
}