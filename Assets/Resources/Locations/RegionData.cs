using System;
using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Region Data")]
public class RegionData : UnityEngine.ScriptableObject
{
    [Reorderable(ReorderableNamingType.ReferencedObjectName)]
    public List<ZoneData> zones;
}