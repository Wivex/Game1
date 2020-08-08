using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


[Serializable]
public class MissionRouteSegment
{
    public ZoneData zone;
    [Tooltip("Represent number of locations required to traverse in this zone in order to reach connected zone")]
    public int length;

    public MissionRouteSegment(ZoneData zone, int length)
    {
        this.zone = zone;
        this.length = length;
    }
}


internal class MissionRoute
{
    internal List<MissionRouteSegment> routeSegments;
    internal ZoneData curZone;
    internal Area curArea;
    internal Sprite curLocSprite;
    internal int curZoneIndex, curLocIndex, remainingSegmentPathLength, transferAreaLength;

    internal MissionRoute(List<MissionRouteSegment> selectedPath)
    {
        routeSegments = new List<MissionRouteSegment>(selectedPath);
        curZone = routeSegments[curZoneIndex].zone;
        curArea = curZone.areas.First();
        remainingSegmentPathLength = routeSegments[curZoneIndex].length;
        SetTransferAreaLength();
    }

    void SetTransferAreaLength()
    {
        // not last zone in route
        if (curZoneIndex < routeSegments.Count - 1)
        {
            var nextSegment = routeSegments[curZoneIndex + 1];
            transferAreaLength = routeSegments[curZoneIndex].zone.areas.Find(area => area.targetZone = nextSegment.zone)
                .locations.Count;
        }
        else
            transferAreaLength = 0;
    }

    internal void NextLocation()
    {
        // not last location in area
        if (curLocIndex < curArea.locations.Count - 1)
        {
            // next location in area
            curLocIndex++;
        }
        // last location in area
        else
        {
            // not yet transfer area
            if (remainingSegmentPathLength > transferAreaLength)
            {
                // next interchangeable area
                curArea = curZone.areas.Where(area => area.type == AreaType.Interchangeable).PickOne();
                curLocIndex = 0;
            }
            // transfer area
            else if (remainingSegmentPathLength > 0 && remainingSegmentPathLength < transferAreaLength)
            {
                var nextZone = routeSegments[curZoneIndex + 1].zone;
                curArea = curZone.areas.Find(
                    area => area.type == AreaType.ZoneTransition && area.targetZone == nextZone);
                curLocIndex = 0;
            }
            // transfer area ended, change zone
            else
            {
                // last zone in route
                if (curZoneIndex >= routeSegments.Count - 1)
                {
                    // TEMP: keep on travelling forever
                    // next interchangeable area
                    curArea = curZone.areas.Where(area => area.type == AreaType.Interchangeable).PickOne();
                    curLocIndex = 0;
                }
                else
                {
                    var nextSegment = routeSegments[curZoneIndex + 1];
                    var entryArea = nextSegment.zone.areas.Find(area => area.targetZone = curZone);
                    if (entryArea == null)
                        Debug.LogError($"Entry area for {curZone.name} in {nextSegment.zone.name} wasn't found");
                    curZoneIndex++;
                    curZone = nextSegment.zone;
                    curArea = entryArea;
                    curLocIndex = 0;
                    SetTransferAreaLength();
                }
            }
        }

        curLocSprite = curArea.locations[curLocIndex];
        remainingSegmentPathLength--;
    }
}