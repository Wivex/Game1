using Game1.Objects.Units;
using Game1.UI.Panels;

namespace Game1.Concepts
{
    public class Expedition
    {
        public string[] Log { get; set; }
        /// <summary>
        /// Time length of current expedition
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Hero, partaking in expedition
        /// </summary>
        public Hero Hero { get; set; }
        /// <summary>
        /// Current enemy of the hero
        /// </summary>
        public Enemy Enemy { get; set; }
        /// <summary>
        /// Current enemy of the hero
        /// </summary>
        public Event Event { get; set; }
        /// <summary>
        /// Current location of hero
        /// </summary>
        public Location Location { get; set; }
        /// <summary>
        /// Current location of hero
        /// </summary>
        public Location Destination { get; set; }

        /// <summary>
        /// Associated panel for this expedition (floating text reference)
        /// </summary>
        public PanelExpeditionOverview ExpeditionOverviewPanel { get; set; }

        public int EnemiesSlain { get; set; }
        public int EXPGot { get; set; }

        public Expedition(Hero hero, string destination)
        {
            Hero = hero;
            Destination = new Location(destination);
            Location = Destination;
            Event = new Travelling(Location);
            Globals.ExpeditionsDict.Add(hero.ID,this);
            Globals.TabExpeditions.AddExpeditionTab(this);
        }

        public void Update()
        {
            Length++;
            if (Event.GetType() == typeof(Travelling)) TryNewEvent();
            else
            {
                Event.Update();
            }
        }

        public void TryNewEvent()
        {
            foreach (var eventData in Location.XMLData.Events)
            {
                if (Globals.RNGesus.NextDouble()<eventData.ChanceToOccur)
                    switch (eventData.Name)
                    {
                        case "EnemyEncounter":
                            Event = new EnemyEncounter(Hero, Location, ExpeditionOverviewPanel);
                            break;
                        default:
                            Event = new Travelling(Location);
                            break;
                    }
            }
        }
    }
}