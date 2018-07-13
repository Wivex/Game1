using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data Templates/Classes/Class Level Data")]
public class ClassLevelData : ScriptableObject
{
    public int health;
    public int mana;
    public int attack;
    public int speed;
    public int defence;
    public int hazardResistance;
    public int bleedResistance;

    public List<AbilityData> abilities;
}