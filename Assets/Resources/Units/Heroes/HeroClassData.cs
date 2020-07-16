using System;
using System.Collections;
using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[Serializable]
public class HeroAnimationSpriteSheets
{
    public List<Sprite> idleAnimation;
    public List<Sprite> moveAnimation;
    public List<Sprite> useAnimation;
    public List<Sprite> attackAnimation;
    public List<Sprite> deathAnimation;
}


[CreateAssetMenu(menuName = "Content/Data/Hero Data")]
public class HeroClassData : UnitData
{
    public ClassType classType;
    public List<Sprite> malePortraits;
    public HeroAnimationSpriteSheets animations;
}