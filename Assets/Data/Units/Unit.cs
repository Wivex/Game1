using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public int healthRegen;

    public int maxMana;
    public int curMana;
    public int manaRegen;

    public int maxInitiative = 100;
    public float curInitiative;

    public int attack;

    public int speed;

    public int defence;
    public int hazardResistance;
    public int bleedResistance;

    #region UI
    public Slider HPSlider, ManaSlider, InitSlider;
    public TextMeshProUGUI HPSliderText, ManaSliderText, InitSliderText;
    #endregion

    void Update()
    {
        HPSliderText.text = $"{curHealth} / {maxHealth}";
        HPSlider.value = (float)curHealth / maxHealth;
        
        ManaSliderText.text = $"{curMana} / {maxMana}";
        ManaSlider.value = (float)curMana / maxMana;

        InitSliderText.text = $"{(int)curInitiative} / {maxInitiative}";
        InitSlider.value = curInitiative / maxInitiative;
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        var floatingText = Instantiate(AssetManager.AM.floatingTextPrefab, transform);
        floatingText.GetComponent<TextMeshProUGUI>().text = $"-{damage}";
    }
}
