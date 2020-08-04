using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Hero Data")]
public class HeroClassData : UnitData
{
    [Header("Hero Properties")]
    public ClassType classType;
    [Tooltip("If female/male sprites order in spritesheet are alike (move: 0-10 -> 50-60), set this offset for later sprite swap based on sex.")]
    public int spriteIndexOffsetBySex = 50;
    [HideInInspector]
    public List<Sprite> portraits, spritesheet;

#if UNITY_EDITOR
    internal override void OnValidate()
    {
        base.OnValidate();

        portraits = AssetHandler.LoadAllNearbyAssets<Sprite>(this, "Portraits");
        spritesheet = AssetHandler.LoadAllNearbyAssets<Sprite>(this, $"{name} Sprite Sheet");
    }
#endif
}