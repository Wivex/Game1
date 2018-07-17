using UnityEngine;

public enum Location
{
    Forest,
    Dungeon
}


public class Expedition
{
    public Hero hero;
    public Event curEvent;
    public Location curLocation, destLocation;
    public RectTransform expeditionPanel;

    public Expedition(Hero hero, Location destination)
    {
        this.hero = hero;
        destLocation = destination;
        curLocation = Location.Forest;
        //Event = new Travelling(LocationData);
    }

    public void Update()
    {
        //Length++;
        //if (Event.GetType() == typeof(Travelling)) TryNewEvent();
        //else
        //{
        //    Event.Update();
        //}
    }

    public void TryNewEvent()
    {
        //foreach (var eventData in LocationData.XMLData.Events)
        //{
        //    if (Globals.RNGesus.NextDouble() < eventData.ChanceToOccur)
        //        switch (eventData.Name)
        //        {
        //            case "EnemyEncounter":
        //                Event = new EnemyEncounter(Hero, LocationData, ExpeditionOverviewPanel);
        //                break;
        //            default:
        //                Event = new Travelling(LocationData);
        //                break;
        //        }
        //}
    }
}