namespace XMLData
{
    public class EventData : EntityData
    {
        public float ChanceToOccur { get; set; }
        public int StartAt { get; set; }
        public int MinFrequency { get; set; }
    }
}