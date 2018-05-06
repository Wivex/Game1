namespace Game1.Concepts
{
    public class Travelling : Event
    {
        public Travelling(Location location)
        {
            Name = $"Travelling through {location.Name}.";
            Texture = location.Texture;
        }

        public override void Update()
        {
        }
    }
}