using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Objects.Units;

namespace Game1.Concepts
{
    public abstract class Event
    {
        public string Name { get; set; }

        public abstract void Update();
    }
}