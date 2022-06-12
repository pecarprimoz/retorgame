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

                Debug.Log (inputComponent.lookDirection);
                foreach (var spriteDirectionEntity in spriteDirection) {
                    SetWeaponSpriteDirection (weaponFilter, weaponPool, offsetPool, spriteDirectionEntity, left);
                    SetPlayerSpriteDirection (playerPool, spriteDirectionEntity, left);
                    SetProjectileSpriteDirection (projectileFilter, projectilePool, offsetPool, spriteDirectionEntity,
                        left);
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
            bool left) {
            if (!weaponPool.Has (spriteDirectionEntity)) return;
            ref var weaponSync = ref weaponPool.Get (spriteDirectionEntity);
            foreach (var wepEntity in weaponFilter) {
                ref var weaponOffsetComponent = ref offsetPool.Get (wepEntity);
                if (left) {
                    weaponSync.trs.gameObject.GetComponent<SpriteRenderer> ().flipY = true;
                    weaponOffsetComponent.offset =
                        new Vector3 (-1 * Mathf.Abs (weaponOffsetComponent.offset.x),
                            weaponOffsetComponent.offset.y,
                            weaponOffsetComponent.offset.z);
                }
                else {
                    weaponSync.trs.gameObject.GetComponent<SpriteRenderer> ().flipY = false;
                    weaponOffsetComponent.offset =
                        new Vector3 (Mathf.Abs (weaponOffsetComponent.offset.x), weaponOffsetComponent.offset.y,
                            weaponOffsetComponent.offset.z);
                }
            }
        }

        // this is trash
        private void SetProjectileSpriteDirection(EcsFilter projectileFilter,
            EcsPool<ProjectileComponent> projectilePool,
            EcsPool<OffsetComponent> offsetPool,
            int spriteDirectionEntity, bool left) {
            if (!projectilePool.Has (spriteDirectionEntity)) return;
            foreach (var projectileEntity in projectileFilter) {
                ref var projectileOffsetComponent = ref offsetPool.Get (projectileEntity);
                ref var projectileComponent = ref projectilePool.Get (projectileEntity);
                if (projectileComponent.trs.gameObject.activeInHierarchy) {
                    if (left) {
                        projectileOffsetComponent.offset =
                            new Vector3 (-1 * Mathf.Abs (projectileOffsetComponent.offset.x),
                                projectileOffsetComponent.offset.y,
                                projectileOffsetComponent.offset.z);
                    }
                    else {
                        projectileOffsetComponent.offset =
                            new Vector3 (Mathf.Abs (projectileOffsetComponent.offset.x),
                                projectileOffsetComponent.offset.y,
                                projectileOffsetComponent.offset.z);
                    }
                }
            }
        }
    }
}