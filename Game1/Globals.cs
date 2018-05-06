using System;
using System.Collections.Generic;
using Game1.Objects.Units;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Concepts
{
    public static class Globals
    {
        public static Game1 Game { get; set; }
        public static Random RNGesus { get; set; } = new Random();
        public static Dictionary<int, Hero> HeroesDict { get; set; }= new Dictionary<int, Hero>();
        public static Dictionary<int, Expedition> ExpeditionsDict { get; set; } = new Dictionary<int, Expedition>();
        
        private static Texture2D MissingTexture { get; set; }

        public static void Init(Game1 game)
        {
            Game = game;
            MissingTexture = Game.Content.Load<Texture2D>(@"Textures/MissingTexture");
        }

        /// <summary>
        /// Loads actual texture or missing texture placeholder
        /// </summary>
        /// <param name="path">Path to the texture folder: @"Textures/Locations/"</param>
        /// <param name="name">Texture name: "Forest"</param>
        /// <returns></returns>
        public static Texture2D TryLoadTexture(string path, string name)
        {
            Texture2D texture;
            try
            {
                texture = Game.Content.Load<Texture2D>(path + name);
            }
            catch
            {
                texture = MissingTexture;
            }
            return texture;
        }
    }
}