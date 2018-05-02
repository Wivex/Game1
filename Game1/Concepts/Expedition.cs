﻿using System;
using Game1.Objects.Units;

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

        public int EnemiesSlain { get; set; }
        public int EXPGot { get; set; }

        public Expedition(Hero hero, string destination)
        {
            Hero = hero;
            Destination = new Location(destination);
            Location = Destination;
            Globals.Expeditions.Add(hero,this);
        }

        public void Update()
        {
            Length++;
            if (Event == null) TryNewEvent();
            else
            {
                Event.Update();
            }
        }

        public Event TryNewEvent()
        {
            foreach (var eventData in Location.LocationData.Events)
            {
                if (Globals.RNGesus.NextDouble()<eventData.ChanceToOccur)
                    switch (eventData.Name)
                    {
                        case "EnemyEncounter":
                            Event = new EnemyEncounter(Hero, Location);
                            break;
                        default:
                            // TODO: change to smth else
                            Event = new EnemyEncounter(Hero, Location);
                            break;
                    }
            }
        }
    }
}