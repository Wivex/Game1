using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Region Data")]
public class RegionData : UnityEngine.ScriptableObject
{
    public List<ZoneData> zones;
}