using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class Unit
{
    [Header("Unit")]
    public Stat[] stats = {
        new StatChanging(),
        new StatChanging(),
        new Stat(),
        new Stat(),
        new Stat(),
        new Stat(),
        new Stat(),
    };

    public List<Ability> abilities = new List<Ability>();
    public List<Effect> curEffects = new List<Effect>();
    public TacticsPreset tacticsPreset;

    public const int reqInitiative = 100;
    public float curInitiative;
    public string name;

    public abstract UnitPanelManager unitPanel { get; }
    public bool Dead => stats[(int) StatType.Health].curValue <= 0;

    public abstract void SetStats();
    public abstract void SetAbilities();

    public void TakeDamage(Damage damage)
    {
        stats[(int) StatType.Health].curValue = Math.Max(stats[(int)StatType.Health].curValue - damage.amount, 0);
        var floatingText = Object.Instantiate(unitPanel.floatingTextPrefab, unitPanel.unitImage.transform);
        var textObject = floatingText.GetComponent<TextMeshProUGUI>();
        textObject.text = $"-{damage.amount}";
        textObject.color = Color.red;
    }

    public void Heal(int amount)
    {
        stats[(int)StatType.Health].curValue = Mathf.Min(stats[(int)StatType.Health].curValue + amount, (stats[(int)StatType.Health]as StatChanging).maxValue);
        var floatingText = Object.Instantiate(unitPanel.floatingTextPrefab, unitPanel.unitImage.transform);
        var textObject = floatingText.GetComponent<TextMeshProUGUI>();
        textObject.text = $"+{Mathf.Abs(amount)}";
        textObject.color = Color.green;
    }
    
    public void AddEffect(Effect effect)
    {
        switch (effect.effectType)
        {
            case EffectType.StatDirectChange:
                switch (effect.stat)
                {
                    case StatType.Health:
                        AffectHealth(effect);
                        break;
                    case StatType.Mana:
                        break;
                    default:
                        throw new NotImplementedException();
                        break;
                }
                break;
            case EffectType.StatModifier:
                effect.ApplyStatModifier(situation);
                break;
        }
    }

    void AffectHealth(Effect effect)
    {
        switch (effect.durationType)
        {
            case DurationType.Instant:
                if (effect.value > 0)
                    DamageTarget(situation);
                if (value < 0)
                    switch (target)
                    {
                        case Target.Self:
                            situation.actor.Heal(value);
                            break;
                        case Target.Foe:
                            situation.target.Heal(value);
                            break;
                    }
                break;
            case DurationType.Persistent:
                situation.hero.curEffects.Add(this);
                break;
            case DurationType.Repeating:
                ApplyStatModifier(situation);
                break;
        }
    }