﻿using System;
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
            var playerFilter = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();

            var projectileFilter = ecsSystems.GetWorld ().Filter<ProjectileComponent> ().End ();
            var projectilePool = ecsSystems.GetWorld ().GetPool<ProjectileComponent> ();


            var weaponFilter = ecsSystems.GetWorld ().Filter<WeaponComponent> ().End ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();

            foreach (var projectileEntity in projectileFilter) {
                ref var projectileComponent = ref projectilePool.Get (projectileEntity);
                if (projectileComponent.trs.gameObject.activeInHierarchy) {
                    projectileComponent.lifetime -= Time.deltaTime;
                    if (projectileComponent.lifetime <= 0) {
                        projectileComponent.lifetime = 5f;
                        projectileComponent.trs.gameObject.SetActive (false);
                    }
                }

                foreach (var playerEntity in playerFilter) {
                    ref var playerInputComponent = ref playerInputPool.Get (playerEntity);
                    foreach (var weaponEntity in weaponFilter) {
                        ref var weaponComponent = ref weaponPool.Get (weaponEntity);
                        if (!projectileComponent.trs.gameObject.activeInHierarchy && playerInputComponent.mouse0 &&
                            weaponComponent.canShoot) {
                            projectileComponent.trs.position = weaponComponent.trs.position;
                            projectileComponent.trs.gameObject.SetActive (true);
                            weaponComponent.canShoot = false;
                            projectileComponent.body.AddForce (playerInputComponent.lookDirection *
                                                               projectileComponent.speed); // set projectile speed
                        }
                    }
                }
            }
        }
    }
}