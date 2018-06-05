namespace Game1.Objects
{
    public abstract class Item : Entity
    {
        public virtual int MaxStackSize => 1;

        public int Cost { get; set; }
        public int StackSize { get; set; }
    }
}