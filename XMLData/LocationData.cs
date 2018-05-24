using System.Collections.Generic;

namespace XMLData
{
    public class LocationData : EntityData
    {
        public List<EventData> Events { get; set; } = new List<EventData>();
        /// <summary>
        /// (EnemyName, ChanceToSpawn)
        /// </summary>
        public Dictionary<string, float> Enemies { get; set; } = new Dictionary<string, float>();
    }
}