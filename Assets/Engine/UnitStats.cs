using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "Content/Data Templates/Stats")]
//public class UnitStats :ScriptableObject
[System.Serializable]
public struct UnitStats
{
    public Stat health;
    public Stat healthRegen;
    public Stat energy;
    public Stat energyRegen;
    public Stat initiative;
    public Stat attack;
    public Stat speed;
    public Stat defence;
    public Stat hazardResistance;
    public Stat bleedResistance;
}

//[System.Serializable]
//public struct UnitStats
//{
//    public int health;
//    public int healthRegen;
//    public int energy;
//    public int energyRegen;
//    public float initiative;
//    public int attack;
//    public int speed;
//    public int defence;
//    public int hazardResistance;
//    public int bleedResistance;
//}