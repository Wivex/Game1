using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Hero Data")]
public class HeroClassData : UnitData
{
    public ClassType classType;
    public List<Sprite> portraits, spritesheet;
    [Tooltip("If female/male sprites order in spritesheet are alike (move: 0-10 -> 50-60), set this offset for later sprite swap based on sex.")]
    public int spriteIndexOffsetBySex;
}