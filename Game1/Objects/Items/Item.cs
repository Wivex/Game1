namespace Game1.Objects
{
    public abstract class Item : Entity
    {
        public uint Cost { get; set; }
        public float DropChance { get; set; }
    }
}