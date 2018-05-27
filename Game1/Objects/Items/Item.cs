namespace Game1.Objects
{
    public abstract class Item : Entity
    {
        public int Cost { get; set; }
        public int Stacksize { get; set; }
    }
}