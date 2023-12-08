using Game.Game.Helpers;

namespace Game
{
    public struct Hit
    {
        public float Damage;
        public float Force;
        public AttackKind Kind;

        public MutableScript<IEffect> Effect;
    }
}
