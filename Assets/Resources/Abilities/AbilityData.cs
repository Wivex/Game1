using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability Data")]
public class AbilityData : ScriptableObject
{
    public Sprite icon;
    public GameObject animationPrefab;
    public int cooldown;
    public int energyCost;
    public EnergyType energyType;
    public List<EffectOverTimeParams> effects;

    void OnValidate()
    {
        foreach (var effect in effects)
        {
            if (effect.effectType != null)
                effect.effectName = effect.effectType.name;

            if (effect.procType == ProcType.Duration && effect.duration < 2)
            {
                effect.duration = 2;
                Debug.LogWarning(
                    $"Duration of {effect.effectType.name} can't be less than 2. Otherwise use Instant Proc Type");
            }

            if (effect.procType == ProcType.DelayedAndDuration && effect.duration < 2)
            {
                effect.duration = 2;
                Debug.LogWarning(
                    $"Duration of {effect.effectType.name} can't be less than 2. Otherwise use Delayed Proc Type");
            }
        }
    }
}