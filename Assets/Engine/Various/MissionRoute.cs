using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


internal class MissionRoute
{
    internal Dictionary<ZoneData, int> path;
    internal ZoneData curZone;
    internal Area curArea;
    internal int curLocationIndex;

    internal MissionRoute(Dictionary<ZoneData, int> path)
    {
        this.path = new Dictionary<ZoneData, int>(path);
        curZone = path.First().Key;
        curArea = curZone.areas.First();
    }
}