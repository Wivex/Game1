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
    public List<EffectParams> effects;

    void OnValidate()
    {
        foreach (var effect in effects)
        {
            if (effect.type != null)
                effect.effectName = effect.type.name;

            if (effect.procType == ProcType.Duration && effect.duration < 2)
            {
                effect.duration = 2;
                Debug.LogWarning(
                    $"Duration of {effect.type.name} can't be less than 2. Otherwise use Instant Proc Type");
            }

            if (effect.procType == ProcType.DelayedAndDuration && effect.duration < 2)
            {
                effect.duration = 2;
                Debug.LogWarning(
                    $"Duration of {effect.type.name} can't be less than 2. Otherwise use Delayed Proc Type");
            }
        }
    }
}