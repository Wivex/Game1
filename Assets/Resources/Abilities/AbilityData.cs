using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability Data")]
public class AbilityData : ScriptableObject
{
    public int cooldown;
    public int energyCost;
    public EnergyType energyType;
    public Sprite icon;
    public GameObject animationPrefab;
    public List<EffectDirectData> effectsDirect;
    public List<EffectOverTimeData> effectsOverTime;

    void OnValidate()
    {
        //foreach (var effect in effectsOverTime)
        //{
        //    if (effect.type != null)
        //        effect.effectName = effect.type.name;

        //    if (effect.procType == ProcType.Duration && effect.duration < 2)
        //    {
        //        effect.duration = 2;
        //        Debug.LogWarning(
        //            $"Duration of {effect.type.name} can't be less than 2. Otherwise use Instant Proc Type");
        //    }

        //    if (effect.procType == ProcType.DelayedAndDuration && effect.duration < 2)
        //    {
        //        effect.duration = 2;
        //        Debug.LogWarning(
        //            $"Duration of {effect.type.name} can't be less than 2. Otherwise use Delayed Proc Type");
        //    }
        //}
    }
}