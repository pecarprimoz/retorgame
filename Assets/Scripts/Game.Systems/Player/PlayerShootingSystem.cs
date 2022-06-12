using System;
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Game.Components;
using Game.Components.Player;
using Game.Data;
using UnityEngine;

namespace Game.Systems {
    public class PlayerShootingSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var playerFilter = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();

            var projectileFilter = ecsSystems.GetWorld ().Filter<ProjectileComponent> ().End ();
            var projectilePool = ecsSystems.GetWorld ().GetPool<ProjectileComponent> ();

            var weaponFilter = ecsSystems.GetWorld ().Filter<WeaponComponent> ().End ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();

            var offsetPool = ecsSystems.GetWorld ().GetPool<OffsetComponent> ();
            foreach (var projectileEntity in projectileFilter) {
                ref var projectileComponent = ref projectilePool.Get (projectileEntity);
                if (!projectileComponent.trs.gameObject.activeInHierarchy) {
                    foreach (var playerEntity in playerFilter) {
                        ref var playerInputComponent = ref playerInputPool.Get (playerEntity);
                        ref var playerComponent = ref playerPool.Get (playerEntity);
                        foreach (var weaponEntity in weaponFilter) {
                            ref var weaponComponent = ref weaponPool.Get (weaponEntity);
                            if (weaponComponent.canShoot && playerInputComponent.mouse0 && !projectileComponent.trs.gameObject.activeInHierarchy) {
                                var offsetComponent = offsetPool.Get (projectileEntity);
                                projectileComponent.trs.position = weaponComponent.trs.position + offsetComponent.offset;
                                projectileComponent.trs.gameObject.SetActive (true);
                                playerComponent.trs.gameObject.GetComponent<Animator> ().SetTrigger ("Shoot");
                                weaponComponent.canShoot = false;
                                projectileComponent.body.AddForce (playerInputComponent.lookDirection *
                                                                   projectileComponent.speed); // set projectile speed
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}