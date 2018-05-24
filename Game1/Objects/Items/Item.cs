using Game1.Concepts;
using XMLData;

namespace Game1.Objects
{
    public abstract class Item : Entity
    {
        public uint Cost { get; set; }
    }
}