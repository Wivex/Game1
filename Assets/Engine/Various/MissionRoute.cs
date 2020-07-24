using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MissionRoute
{
    public Texture2D baseTexture;
    public AreaType type = AreaType.Interchangeble;
    [HideIfNotEnumValues("type", AreaType.ZoneTransition)]
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