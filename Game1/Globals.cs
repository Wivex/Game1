using System;
using System.Collections.Generic;
using Game1.Objects.Units;
using Game1.UI.Panels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Concepts
{
    public static class Globals
    {
        /// <summary>
        /// Location of a directory, containing all game data files (inside Content directory)
        /// </summary>
        public static string DataPathBase = "Game1 Data";

        public static Game1 Game { get; set; }
        public static Random RNGesus { get; set; } = new Random();
        public static Dictionary<int, Hero> HeroesDict { get; set; }= new Dictionary<int, Hero>();
        public static Dictionary<int, Expedition> ExpeditionsDict { get; set; } = new Dictionary<int, Expedition>();
        public static TabExpeditions TabExpeditions { get; set; }
        public static GameTime GameTime { get; set; }

        public static Texture2D MissingTexture { get; set; }

        public static void Init(Game1 game)
        {
            Game = game;
            MissingTexture = Game.Content.Load<Texture2D>($@"{DataPathBase}\MissingTexture");
        }

        /// <summary>
        /// Loads actual texture or missing texture placeholder
        /// </summary>
        /// <param name="texturePath">Full path and name relative to Content folder: @"Game1 Data/Locations/Forest"</param>
        /// <returns></returns>
        public static Texture2D TryLoadTexture(string texturePath)
        {
            try
            {
                return Game.Content.Load<Texture2D>(texturePath);
            }
            catch
            {
                return MissingTexture;
            }
        }

        /// <summary>
        /// Loads actual texture or missing texture placeholder
        /// </summary>
        /// <param name="dataPath">Full path and name relative to Content folder: @"Game1 Data/Locations/Forest"</param>
        /// <returns></returns>
        public static T TryLoadData<T>(string dataPath)
        {
            try
            {
                return Game.Content.Load<T>(dataPath);
            }
            catch
            {
                throw new Exception($@"Can't load XMLData for {dataPath}");
            }
        }
    }
}