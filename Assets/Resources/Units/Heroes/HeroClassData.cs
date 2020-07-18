using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;
using UnityEditor;
using UnityEngine;

internal class HeroSprites
{
    internal List<Sprite> portraits;

    internal List<Sprite> idleAnimationFrames;
    internal List<Sprite> moveAnimationFrames;
    internal List<Sprite> useAnimationFrames;
    internal List<Sprite> attackAnimationFrames;
    internal List<Sprite> deathAnimationFrames;
}


[CreateAssetMenu(menuName = "Content/Data/Hero Data")]
public class HeroClassData : UnitData
{
    public ClassType classType;

    internal Dictionary<SexType, HeroSprites> sprites = new Dictionary<SexType, HeroSprites>();

    /// <summary>
    /// Called when: SO created/game started/SO clicked (once between game runs, if wasn't selected already)
    /// </summary>
    void OnEnable()
    {
        // skip loading assets if SO is just created and variables are not yet set
        if (name == string.Empty) return;

        var allAnimationSprites = AssetHandler.LoadAllNearbyAssets<Sprite>(this, $"{name} Sprite Sheet");
        var allPortraits = AssetHandler.LoadAllNearbyAssets<Sprite>(this, "Portraits");

        // load assets based on sex
        sprites.Clear();
        foreach (SexType sex in Enum.GetValues(typeof(SexType)))
        {
            sprites.Add(sex, new HeroSprites
            {
                portraits = allPortraits.Where(sprite => sprite.name.Contains($"{sex}")).ToList(),
                idleAnimationFrames = allAnimationSprites.Where(sprite => sprite.name.Contains($"{sex} Idle")).ToList(),
                moveAnimationFrames = allAnimationSprites.Where(sprite => sprite.name.Contains($"{sex} Move")).ToList(),
                useAnimationFrames = allAnimationSprites.Where(sprite => sprite.name.Contains($"{sex} Use")).ToList(),
                attackAnimationFrames =
                    allAnimationSprites.Where(sprite => sprite.name.Contains($"{sex} Attack")).ToList(),
                deathAnimationFrames =
                    allAnimationSprites.Where(sprite => sprite.name.Contains($"{sex} Death")).ToList()
            });
        }
    }
}