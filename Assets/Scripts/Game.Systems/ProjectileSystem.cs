using System;
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Game.Components;
using Game.Components.Player;
using Game.Data;
using UnityEngine;

namespace Game.Systems {
    public class ProjectileSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var gameData = ecsSystems.GetShared<GameData> ();
            if (gameData.gameConfig.playerConfig.weaponConfiguration.projectileConfiguration == null) {
                return;
            }

            var playerFilter = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();

            var projectileFilter = ecsSystems.GetWorld ().Filter<ProjectileComponent> ().End ();
            var projectilePool = ecsSystems.GetWorld ().GetPool<ProjectileComponent> ();

            var weaponFilter = ecsSystems.GetWorld ().Filter<WeaponComponent> ().End ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();

            var offsetPool = ecsSystems.GetWorld ().GetPool<OffsetComponent> ();
            
            var projectileLifetime =
                gameData.gameConfig.playerConfig.weaponConfiguration.projectileConfiguration.lifetime;

            // handle projectile lifetime 
            foreach (var projectileEntity in projectileFilter) {
                ref var projectileComponent = ref projectilePool.Get (projectileEntity);
                if (projectileComponent.trs.gameObject.activeInHierarchy) {
                    projectileComponent.lifetime -= Time.deltaTime;
                    if (projectileComponent.lifetime <= 0) {
                        projectileComponent.lifetime = projectileLifetime;
                        projectileComponent.trs.gameObject.SetActive (false);
                    }
                }
            }

            foreach (var weaponEntity in weaponFilter) {
                ref var weaponComponent = ref weaponPool.Get (weaponEntity);
                int spawnedProjectileCount = 0;
                float yOffset = 0f;
                foreach (var projectileEntity in projectileFilter) {
                    if (spawnedProjectileCount >= weaponComponent.projectileSpawnCount) {
                        weaponComponent.canShoot = false;
                        break;
                    }

                    foreach (var playerEntity in playerFilter) {
                        ref var playerInput = ref playerInputPool.Get (playerEntity);
                        ref var projectileComponent = ref projectilePool.Get (projectileEntity);
                        if (!projectileComponent.trs.gameObject.activeInHierarchy && playerInput.mouse0 &&
                            weaponComponent.canShoot) {
                            var offsetComponent = offsetPool.Get (weaponEntity);
                            projectileComponent.trs.position = weaponComponent.trs.position + offsetComponent.offset;
                            projectileComponent.trs.gameObject.SetActive (true);
                            projectileComponent.body.AddForce ((playerInput.lookDirection + new Vector3 (0, yOffset, 0)) *
                                                               projectileComponent.speed); // set projectile speed
                            Debug.Log ($"Spawn projectile count {spawnedProjectileCount}");
                            spawnedProjectileCount++;
                        }
                    }

                    yOffset += 0.03f;
                }
            }
        }
    }
}