
using EditorPlus;

namespace Game
{
    public struct Hit
    {
        public float Damage;
        public float Force;
        public AttackKind Kind;

        public IImplementation<IEffect> Effect;
    }
}
