using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SiteType
{
    Interchangeble,
    ZoneTransition
}

[Serializable]
public class Site
{
    public Texture2D baseTexture;
    public SiteType type = SiteType.Interchangeble;
    [HideIfNotEnumValues("type", SiteType.ZoneTransition)]
    public ZoneData targetZone;

    /// <summary>
    /// These site images are always traversed one after another (based on site pieces formed from original site image
    /// </summary>
    internal List<Sprite> siteChainSprites;

    public void LoadChainSprites()
    {
        siteChainSprites?.Clear();
        siteChainSprites = new List<Sprite>(AssetHandler.LoadChildAssets<Sprite>(baseTexture));
    }

}