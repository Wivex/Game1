using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocationPanelDrawer : MonoBehaviour
{
    [HideInInspector]
    public LocationData location;

    public Image locImage;
    public TextMeshProUGUI locName;

    public Transform situationsPanel,
        enemiesPanel;

    public List<Image> situationsIcons,
        enemiesIcons;

    public List<TextMeshProUGUI> situationsName,
        situationsChance,
        enemiesName,
        enemiesChance;

    void OnValidate()
    {
        var childImages = new List<Image>();
        var childTexts = new List<TextMeshProUGUI>();

        situationsPanel.gameObject.GetComponentsInChildren(true, childImages);
        situationsIcons = childImages.FindAll(image => image.gameObject.name.Contains("Image"));
        situationsPanel.gameObject.GetComponentsInChildren(true, childTexts);
        situationsName = childTexts.FindAll(text => text.gameObject.name.Contains("Name"));
        situationsChance = childTexts.FindAll(text => text.gameObject.name.Contains("Chance"));

        enemiesPanel.gameObject.GetComponentsInChildren(true, childImages);
        enemiesIcons = childImages.FindAll(image => image.gameObject.name.Contains("Image"));
        enemiesPanel.gameObject.GetComponentsInChildren(true, childTexts);
        enemiesName = childTexts.FindAll(text => text.gameObject.name.Contains("Name"));
        enemiesChance = childTexts.FindAll(text => text.gameObject.name.Contains("Chance"));
    }

    void Update()
    {
        // update situations
        for (var i = 0; i < situationsIcons.Count; i++)
        {
            if (i >= location.situations.Count)
            {
                situationsIcons[i].sprite = null;
                situationsIcons[i].color = Color.clear;
                situationsName[i].text = string.Empty;
            }
            else
            {
                situationsIcons[i].sprite = location.situations[i].icon;
                situationsIcons[i].color = Color.white;
                situationsName[i].text = location.situations[i].name;
            }
        }
    }
}