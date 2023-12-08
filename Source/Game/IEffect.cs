namespace Game
{
    public interface IEffect
    {
        public float Duration { get;}
        public string Name { get;}
        public void Start(EntityDefense defense); //Does anything when starts
        public void End(EntityDefense defense); //Reverts
    }
}
