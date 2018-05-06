using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Objects;
using Game1.Objects.Units;

namespace Game1.Concepts
{
    public abstract class Event : Entity
    {
        public abstract void Update();
    }
}