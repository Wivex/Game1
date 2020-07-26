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
    internal List<MissionRouteSegment> segments;
    internal ZoneData curZone;
    internal Area curArea;
    internal int curSegIndex, curLocIndex, remainingSegmentLength, transferAreaLength;

    void SetTransferAreaLength()
    {
        if (curSegIndex < segments.Count - 1)
        {
            var nextSegment = segments[curSegIndex + 1];
            transferAreaLength = segments[curSegIndex].zone.areas.Find(area => area.targetZone = nextSegment.zone)
                .locations.Count;
        }
        else
        {
            transferAreaLength = 0;
        }
    }

    internal MissionRoute(List<MissionRouteSegment> selectedPath)
    {
        segments = new List<MissionRouteSegment>(selectedPath);
        curZone = segments[curSegIndex].zone;
        curArea = curZone.areas.First();
        remainingSegmentLength = segments[curSegIndex].length;
    }

    internal Sprite NextLocationSprite()
    {
        // not last location in area
        if (curLocIndex < curArea.locations.Count - 1)
        {
            // next location in area
            curLocIndex++;
        }
        else
        {
            // not yet time for transfer area or last segment in route
            if (remainingSegmentLength > transferAreaLength || curSegIndex >= segments.Count - 1)
            {
                // next interchangeable area
                curArea = curZone.areas.Where(area => area.type == AreaType.Interchangeable).PickOne();
                curLocIndex = 0;
            }
            else
            {
                // find transfer area
                var nextZone = segments[curSegIndex + 1].zone;
                curArea = curZone.areas.Find(
                    area => area.type == AreaType.ZoneTransition && area.targetZone == nextZone);
                curLocIndex = 0;
            }
        }

        remainingSegmentLength--;
        return curArea.locations[curLocIndex];
    }
}