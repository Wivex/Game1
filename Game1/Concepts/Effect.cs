using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Engine;
using Game1.Objects;
using XMLData;

namespace Game1.Concepts
{
    public class Effect
    {
        public int Duration { get; set; }

        public EffectData XMLData { get; set; }

        public Effect(EffectData data)
        {
            XMLData = data;

            Duration = XMLData.Duration;
        }
    }
}
