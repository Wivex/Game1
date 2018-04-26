namespace Game1.Concepts
{
    public class Stat
    {
        public Stat(string name, float value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public float Value { get; set; }
        //public float FinalValue { get; set; }
        //public Dictionary<string, int> Modifiers { get; } = new Dictionary<string, int>();

        //public void AddModifier(string sourceName, int value)
        //{
        //    // auto added if new, else updated
        //    Modifiers[sourceName] = value;
        //}

        //public void RemoveModifier(string sourceName)
        //{
        //    // no need to check if exists
        //    Modifiers.Remove(sourceName);
        //}

        //public float Finalized()
        //{
        //    FinalValue = Value;
        //    foreach (var elem in Modifiers)
        //    {
        //        FinalValue += elem.Value;
        //    }
        //    return FinalValue;
        //}
    }
}
