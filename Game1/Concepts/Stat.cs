namespace Game1.Concepts
{
    public static class Stat
    {
        public static string Health { get; } = "Health";
        /// <summary>
        /// Physical attack value. Reduced by defence
        /// </summary>
        public static string Attack { get; } = "Attack";
        /// <summary>
        /// Physical defence value. Reduces physical attack damage
        /// </summary>
        public static string Defence { get; } = "Defence";
        /// <summary>
        /// Magical/elemental defence value. Reduces magical/elemental damage
        /// </summary>
        public static string Resistance { get; } = "Resistance";
        /// <summary>
        /// Defines frequency of unit turns
        /// </summary>
        public static string Speed { get; } = "Speed";

        /// <summary>
        /// Energy resource for magical skills
        /// </summary>
        public static string Mana { get; } = "Mana";
        /// <summary>
        /// Energy resource for fighting skills
        /// </summary>
        public static string Stamina { get; } = "Stamina";
    }
}