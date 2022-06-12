using Game.Components;
using Game.Components.Player;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems {
    public class SpriteDirectionSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var spriteDirection = ecsSystems.GetWorld ().Filter<SpriteDirectionComponent> ().End ();
            var weaponFilter = ecsSystems.GetWorld ().Filter<WeaponComponent> ().End ();
            var projectileFilter = ecsSystems.GetWorld ().Filter<ProjectileComponent> ().End ();
            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();
            var offsetPool = ecsSystems.GetWorld ().GetPool<OffsetComponent> ();
            var projectilePool = ecsSystems.GetWorld ().GetPool<ProjectileComponent> ();

            foreach (var _ in ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ()) {
                ref var inputComponent = ref playerInputPool.Get (_);
                bool left = inputComponent.lookDirection.x < 0;
                bool up = inputComponent.lookDirection.y < 0.25f;
                Debug.Log (inputComponent.lookDirection);
                foreach (var spriteDirectionEntity in spriteDirection) {
                    SetWeaponSpriteDirection (weaponFilter, weaponPool, offsetPool, spriteDirectionEntity, left, up);
                    SetPlayerSpriteDirection (playerPool, spriteDirectionEntity, left);
                }
            }
        }

        private void SetPlayerSpriteDirection(EcsPool<PlayerComponent> playerPool, int spriteDirectionEntity,
            bool left) {
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

        private void SetWeaponSpriteDirection(EcsFilter weaponFilter,
            EcsPool<WeaponComponent> weaponPool, EcsPool<OffsetComponent> offsetPool, int spriteDirectionEntity,
            bool left, bool up) {
            if (!weaponPool.Has (spriteDirectionEntity)) return;
            ref var weaponSync = ref weaponPool.Get (spriteDirectionEntity);
            foreach (var wepEntity in weaponFilter) {
                ref var weaponOffsetComponent = ref offsetPool.Get (wepEntity);
                var weaponSpriteRendererRef = weaponSync.trs.gameObject.GetComponent<SpriteRenderer> ();
                if (left) {
                    weaponSpriteRendererRef.flipY = true;
                    weaponOffsetComponent.offset =
                        new Vector3 (-1 * Mathf.Abs (weaponOffsetComponent.offset.x),
                            weaponOffsetComponent.offset.y,
                            weaponOffsetComponent.offset.z);
                }
                else {
                    weaponSpriteRendererRef.flipY = false;
                    weaponOffsetComponent.offset =
                        new Vector3 (Mathf.Abs (weaponOffsetComponent.offset.x), weaponOffsetComponent.offset.y,
                            weaponOffsetComponent.offset.z);
                }

                if (up) {
                    weaponSpriteRendererRef.sortingOrder = 2;
                }
                else {
                    weaponSpriteRendererRef.sortingOrder = 0;
                }
            }
        }
    }
}