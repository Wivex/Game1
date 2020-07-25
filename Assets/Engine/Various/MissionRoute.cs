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
        curArea = segments[curSegIndex].zone.areas.First();
        remainingSegmentLength = segments[curSegIndex].length;
    }



    internal Sprite NextLocationSprite()
    {
        // not yet in transfer area
        if (remainingSegmentLength > transferAreaLength)
        {
            if (curLocIndex >= curArea.locations.Count - 1)
            {

            }
        }
        else
        {

        }

        return null;
    }
}