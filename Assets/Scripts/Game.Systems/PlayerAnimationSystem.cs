using Leopotam.EcsLite;
using Game.Components.Player;
using UnityEngine;

namespace Game.Systems {
    public class PlayerAnimationSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var filter = ecsSystems.GetWorld ().Filter<PlayerInputComponent> ().End ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();
            var playerComponentPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();

            foreach (var entity in filter) {
                ref var playerInputComponent = ref playerInputPool.Get (entity);
                ref var playerComponent = ref playerComponentPool.Get (entity); 
                var playerAnimator = playerComponent.trs.gameObject.GetComponent<Animator> ();
                playerAnimator.SetBool ("Walking", playerInputComponent.IsMoving);
                if (playerInputComponent.IsShooting) {
                    playerAnimator.SetTrigger ("Shoot");
                }
            }
        }
    }
}