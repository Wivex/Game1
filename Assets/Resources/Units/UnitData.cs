using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

public abstract class UnitData : ScriptableObject
{
    [Header("Unit Properties")]
    public StatsSheet baseStats = new StatsSheet
    {
        health = 100,
        energy = 100,
        attack = 10,
        defence = 5,
        speed = 10
    };
    public EnergyType energyType = EnergyType.Mana;
    public List<AbilityData> abilities;
    public List<Tactic> tactics;
}