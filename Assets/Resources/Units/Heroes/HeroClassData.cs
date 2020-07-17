using System;
using System.Collections;
using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEditor;
using UnityEngine;

[Serializable]
public class HeroAnimationSpriteSheets
{
    public List<Sprite> idleAnimation = new List<Sprite>();
    public List<Sprite> moveAnimation = new List<Sprite>();
    public List<Sprite> useAnimation = new List<Sprite>();
    public List<Sprite> attackAnimation = new List<Sprite>();
    public List<Sprite> deathAnimation = new List<Sprite>();
}


[CreateAssetMenu(menuName = "Content/Data/Hero Data")]
public class HeroClassData : UnitData
{
    public ClassType classType;

    internal Dictionary<SexType, HeroAnimationSpriteSheets> animations = new Dictionary<SexType, HeroAnimationSpriteSheets>();
    internal Dictionary<SexType, List<Sprite>> portraits = new Dictionary<SexType, List<Sprite>>();

    new void OnEnable()
    {
        // skip loading assets if SO is just created and variables are not yet set
        if (name == string.Empty) return;

        var allAnimationSprites = AssetHandler.LoadAllNearbyAssets<Sprite>(this, " spritesheet");

        // animations.Add(SexType.Male, new HeroAnimationSpriteSheets()
        // {
        //
        // });
    }
}