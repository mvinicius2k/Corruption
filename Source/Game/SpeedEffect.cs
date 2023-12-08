

using FlaxEngine;

namespace Game
{
    public struct SpeedEffect : IEffect
    {
        public float Duration;
        public string Name;
        public float Amount;
        float IEffect.Duration => Duration;
        string IEffect.Name => Name;

        public void Start(EntityDefense defense)
        {
            defense.Entity.EntityMovement.Speed.Multiplicators.Add(Amount); //Slows or boosts the player speed
            Debug.Log($"Efeito de velocidade em {defense.Entity.Actor.Name}");


        }
        public void End(EntityDefense defense)
        {
            defense.Entity.EntityMovement.Speed.Multiplicators.Remove(Amount); //Reverts
            Debug.Log($"Revertendo efeito de velocidade em {defense.Entity.Actor.Name}");

        }
    }
}
