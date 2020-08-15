using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    [Header("Unit Properties")]
    public EnergyType energyType = EnergyType.Mana;
    public StatsSheet stats = new StatsSheet
    {
        health = 100,
        energy = 100,
        attack = 10,
        defence = 5,
        speed = 10
    };
    public List<AbilityData> abilities;
    public List<Tactic> tactics;
    [HideInInspector]
    public List<string> abilitiesNames;
    
#if UNITY_EDITOR
    internal virtual void OnValidate()
    {
        // verify that each tactic has at least 1 trigger
        foreach (var tactic in tactics.Where(tactic => !tactic.triggers.NotNullOrEmpty()))
        {
            tactic.triggers = new List<TacticTrigger>
            {
                new TacticTrigger
                {
                    triggerType = TriggerType.Any
                }
            };
        }

        // remove empty elements in list after removing element content object
        abilities.RemoveAll(slot => slot == null);

        abilitiesNames = new List<string>();
        foreach (var abilityData in abilities)
        {
            abilitiesNames.Add(abilityData.name);
        }

        // required to be able to save script changes to SO to an actual asset file (only inspector changes are saved by default)
        EditorUtility.SetDirty(this);
    }
#endif
}