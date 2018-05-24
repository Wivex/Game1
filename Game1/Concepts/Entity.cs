using Game1.Concepts;
using Microsoft.Xna.Framework.Graphics;
using XMLData;

namespace Game1.Objects
{
    public abstract class Entity
    {
        public string Name { get; set; }
        /// <summary>
        /// Should be overriden in child class, e.g.: [Equipment]/Swords/Long Sword
        /// </summary>
        public virtual string DataClassPath { get; }
        public Texture2D Texture { get; set; }
    }
}