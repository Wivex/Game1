using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionFrameDrawer : MonoBehaviour
{
    public Image locImage;
    public TextMeshProUGUI expLabel;

    public void Init(LocationData locData)
    {
        locImage.sprite = locData.icon;
        expLabel.text = $"Explore\nthe {locData.name}";
    }
}