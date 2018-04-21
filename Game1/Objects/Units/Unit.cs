namespace Game1.Objects
{
    public abstract class Unit : Entity
    {
        public uint Health { get; set; }
        public uint Attack { get; set; }
        public uint Defence { get; set; }
        public uint Speed { get; set; }

        public bool Action { get; set; }

        public Unit()
        {
            Health = 100;
            Attack = 10;
            Defence = 5;
            Speed = 3;
        }
    }
}
