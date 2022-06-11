using Game.Components;
using Game.Components.Player;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems {
    public class SpriteDirectionSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var spriteDirection = ecsSystems.GetWorld ().Filter<SpriteDirectionComponent> ().End ();
            var spriteDirectionPool = ecsSystems.GetWorld ().GetPool<SpriteDirectionComponent> ();
            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();
            foreach (var _ in ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ()) {
                ref var inputComponent = ref playerInputPool.Get (_);
                bool left = false;
                if (inputComponent.lookDirection.x < 0) {
                    left = true;
                }

                Debug.Log (inputComponent.lookDirection);
                foreach (var spriteDirectionEntity in spriteDirection) {
                    if (weaponPool.Has (spriteDirectionEntity)) {
                        ref var weaponSync = ref weaponPool.Get (spriteDirectionEntity);
                        if (left) {
                            weaponSync.trs.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
                        }
                        else {
                            weaponSync.trs.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
                        }
                    }

                    if (playerPool.Has (spriteDirectionEntity)) {
                        ref var playerSync = ref playerPool.Get (spriteDirectionEntity);
                        if (left) {
                            playerSync.trs.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
                        }
                        else {
                            playerSync.trs.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
                        }
                    }
                }
            }
        }
    }
}
