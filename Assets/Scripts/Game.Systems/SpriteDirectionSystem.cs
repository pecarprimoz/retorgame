using Game.Components;
using Game.Components.Player;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems {
    public class SpriteDirectionSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var spriteDirection = ecsSystems.GetWorld ().Filter<SpriteDirectionComponent> ().End ();
            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();
            var offsetPool = ecsSystems.GetWorld ().GetPool<OffsetComponent> ();

            foreach (var _ in ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ()) {
                ref var inputComponent = ref playerInputPool.Get (_);
                bool left = inputComponent.lookDirection.x < 0;

                Debug.Log (inputComponent.lookDirection);
                foreach (var spriteDirectionEntity in spriteDirection) {
                    if (weaponPool.Has (spriteDirectionEntity)) {
                        ref var weaponSync = ref weaponPool.Get (spriteDirectionEntity);
                        foreach (var wepEntity in ecsSystems.GetWorld ().Filter<WeaponComponent> ().End ()) {
                            if(!offsetPool.Has (wepEntity)) continue;
                            ref var offsetComponent = ref offsetPool.Get (wepEntity);
                            if (left) {
                                weaponSync.trs.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
                                offsetComponent.offset =
                                    new Vector3 (-1 * Mathf.Abs (offsetComponent.offset.x), offsetComponent.offset.y,
                                        offsetComponent.offset.z);
                            }
                            else {
                                weaponSync.trs.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
                                offsetComponent.offset =
                                    new Vector3 (Mathf.Abs (offsetComponent.offset.x), offsetComponent.offset.y,
                                        offsetComponent.offset.z);
                            }
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