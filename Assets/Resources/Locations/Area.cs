using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AreaType
{
    Interchangeable,
    ZoneTransition
}

[Serializable]
public class Area
{
    public Texture2D areaTexture;
    public AreaType type = AreaType.Interchangeable;
    [HideIfNotEnumValues("type", AreaType.ZoneTransition)]
    public ZoneData targetZone;

    /// <summary>
    /// These images are always traversed one after another (based on order of sprites formed from original area texture
    /// </summary>
    internal List<Sprite> locations;

    public void LoadLocations()
    {
        locations?.Clear();
        locations = new List<Sprite>(AssetHandler.LoadChildAssets<Sprite>(areaTexture));
    }

}