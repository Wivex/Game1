using System.Collections.Generic;

namespace Game1.Concepts
{
    public class Stat
    {
        public static string Health => "Health";
        /// <summary>
        /// Physical attack value. Reduced by defence
        /// </summary>
        public static string Attack => "Attack";
        /// <summary>
        /// Physical defence value. Reduces physical attack damage
        /// </summary>
        public static string Defence => "Defence";
        /// <summary>
        /// Magical/elemental defence value. Reduces magical/elemental damage
        /// </summary>
        public static string Resistance => "Resistance";
        /// <summary>
        /// Defines frequency of unit turns
        /// </summary>
        public static string Speed => "Speed";
        /// <summary>
        /// Energy resource for magical skills
        /// </summary>
        public static string Mana => "Mana";
        /// <summary>
        /// Energy resource for fighting skills
        /// </summary>
        public static string Stamina => "Stamina";

        public string Name { get; set; }

        private int BaseValue { get; set; }
        
        public int Value
        {
            get
            {
                var value = BaseValue;
                if (Modifiers != null)
                    foreach (var modifier in Modifiers)
                        value += modifier.Value;
                return value;
            }
            set => BaseValue = value;
        }

        public Stat(string name, int baseValue)
        {
            Name = name;
            BaseValue = baseValue;
        }

        /// <summary>
        /// [source, value]
        /// </summary>
        public Dictionary<string, int> Modifiers { get; set; }


        #region OPERATORS
        public static bool operator <(Stat a, Stat b)
        {
            return a.Value < b.Value;
        }
        public static bool operator >(Stat a, Stat b)
        {
            return a.Value > b.Value;
        }
        public static int operator *(Stat a, Stat b)
        {
            return a.Value * b.Value;
        }
        public static int operator *(Stat a, int b)
        {
            return a.Value * b;
        }
        public static int operator *(Stat a, double b)
        {
            return (int)(a.Value * b);
        }
        public static int operator +(Stat a, Stat b)
        {
            return a.Value + b.Value;
        }
        public static int operator +(Stat a, int b)
        {
            return a.Value + b;
        }
        public static int operator +(Stat a, double b)
        {
            return (int)(a.Value + b);
        }
        public static int operator -(Stat a, Stat b)
        {
            return a.Value - b.Value;
        }
        public static int operator -(Stat a, int b)
        {
            return a.Value - b;
        }
        public static int operator -(Stat a, double b)
        {
            return (int)(a.Value - b);
        }
        #endregion
    }
}