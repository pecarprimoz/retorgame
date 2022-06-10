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
                playerComponent.trs.gameObject.GetComponent<Animator> ()
                    .SetBool ("Walking", playerInputComponent.IsMoving);
            }
        }
    }
}