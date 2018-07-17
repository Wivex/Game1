using System.Collections.Generic;
using UnityEngine;

public class LocationData : ScriptableObject
{
    public List<Enemy> enemies;
    public List<Event> events;
    public List<string> pointsOfInterest;
}
