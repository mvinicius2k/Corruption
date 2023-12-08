using FlaxEngine;
using Game.Game;
using Game.Game.Helpers;

namespace Game
{

    

    public class Trap : Script
    {
       
        public Collider ColliderTrigger;
        public Hit hit;


      
        public void Attack(PhysicsColliderActor target)
        {
            var actorRoot = target.GetScript<GotoRoot>();
            if (actorRoot == null)
                return;

            var defense = actorRoot.Root.GetScript<EntityDefense>();
            

            if (defense != null)
            {
                Debug.Log($"{defense.Actor.Name} Caiu na armadilha", this);
                defense.DigestAttack(hit);
                this.Enabled = false;
            }


        }

        public override void OnUpdate()
        {
            //Init();
        }

        public override void OnEnable()
        {
            
            ColliderTrigger.TriggerEnter += Attack;
        }

        public override void OnDisable()
        {
            ColliderTrigger.TriggerEnter -= Attack;
        }


    }
}
