using UnityEngine;

public class Expedition : MonoBehaviour
{
    //public string[] log;
    //public int length;

    /// <summary>
    /// Hero, partaking in expedition
    /// </summary>
    public Hero hero;
    /// <summary>
    /// Current enemy of the hero
    /// </summary>
    public Enemy enemy;
    /// <summary>
    /// Current enemy of the hero
    /// </summary>
    public Event curEvent;
    /// <summary>
    /// Current location of hero
    /// </summary>
    public Location curLocation;
    /// <summary>
    /// Current location of hero
    /// </summary>
    public Location destLocation;

    /// <summary>
    /// Associated panel for this expedition (floating text reference)
    /// </summary>
    //public PanelExpeditionOverview ExpeditionOverviewPanel { get; set; }

    //public int EnemiesSlain { get; set; }
    //public int EXPGot { get; set; }

    //public Expedition(Hero hero, string destination)
    //{
    //    Hero = hero;
    //    Destination = new Location(destination);
    //    Location = Destination;
    //    Event = new Travelling(Location);
    //    Globals.ExpeditionsDict.Add(hero.ID, this);
    //    Globals.TabExpeditions.AddExpeditionTab(this);
    //}

    //public void Update()
    //{
    //    Length++;
    //    if (Event.GetType() == typeof(Travelling)) TryNewEvent();
    //    else
    //    {
    //        Event.Update();
    //    }
    //}

    //public void TryNewEvent()
    //{
    //    foreach (var eventData in Location.XMLData.Events)
    //    {
    //        if (Globals.RNGesus.NextDouble() < eventData.ChanceToOccur)
    //            switch (eventData.Name)
    //            {
    //                case "EnemyEncounter":
    //                    Event = new EnemyEncounter(Hero, Location, ExpeditionOverviewPanel);
    //                    break;
    //                default:
    //                    Event = new Travelling(Location);
    //                    break;
    //            }
    //    }
    //}
}