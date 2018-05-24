namespace Game1.Concepts
{
    public static class Stat
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
    }
}