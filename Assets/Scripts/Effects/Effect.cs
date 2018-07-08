using UnityEngine;

namespace Game
{
    public abstract class Effect : MonoBehaviour
    {
        public enum Target
        {
            Self,
            Other
        }

        public Target target;

        public abstract void Apply(ref UnitStats stats);
        public abstract void Unapply(ref UnitStats stats);
    }
}