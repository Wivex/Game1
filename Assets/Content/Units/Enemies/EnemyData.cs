using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data Templates/Enemy Data")]
public class EnemyData : ContentData
{
    public float spawnChance;

    public int health;
    public int mana;
    public int attack;
    public int speed;
    public int defence;
    public int hazardResistance;
    public int bleedResistance;

    public List<AbilityData> abilities;
}