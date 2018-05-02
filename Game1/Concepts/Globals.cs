using System;
using System.Collections.Generic;
using Game1.Objects.Units;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Concepts
{
    public static class Globals
    {
        public static Game1 Game { get; set; }
        public static Dictionary<string, Hero> Heroes { get; set; }= new Dictionary<string, Hero>();
        public static Dictionary<Hero, Enemy> Enemies { get; set; } = new Dictionary<Hero, Enemy>();
        public static Dictionary<Hero, Expedition> Expeditions { get; set; } = new Dictionary<Hero, Expedition>();


        private static Texture2D MissingTexture { get; set; }
        public static Random RNGesus { get; set; } = new Random();

        /// <summary>
        /// Loads actual texture or missing texture placeholder
        /// </summary>
        /// <param name="path">Path to the texture folder: @"Textures/Locations/"</param>
        /// <param name="name">Texture name: "Forest"</param>
        /// <returns></returns>
        public static Texture2D TryLoadTexture(string path, string name)
        {
            return Game.Content.Load<Texture2D>(path + name) ?? MissingTexture;
        }
    }
}