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
            var playerFilter = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();
            var crosshairPool = ecsSystems.GetWorld ().GetPool<CrosshairComponent> ();

            var projectileFilter = ecsSystems.GetWorld ().Filter<ProjectileComponent> ().End ();
            var projectilePool = ecsSystems.GetWorld ().GetPool<ProjectileComponent> ();
            foreach (var _ in playerFilter) {
                ref var playerComponent = ref playerPool.Get (_);
                ref var playerInputComponent = ref playerInputPool.Get (_);
                ref var weaponComponent = ref weaponPool.Get (_);
                ref var crosshairComponent = ref crosshairPool.Get (_);
                
                foreach (var projectileEntity in projectileFilter) {
                    ref var projectileComponent = ref projectilePool.Get (projectileEntity);
                    if (!projectileComponent.trs.gameObject.activeInHierarchy && playerInputComponent.mouse0) {
                        
                        projectileComponent.trs.position = weaponComponent.trs.position;
                        projectileComponent.trs.gameObject.SetActive (true);
                        projectileComponent.body.AddForce (playerInputComponent.lookDirection * projectileComponent.speed); // set projectile speed
                        break;
                    }

                    projectileComponent.lifetime -= Time.deltaTime;
                    if (projectileComponent.lifetime <= 0) {
                        projectileComponent.lifetime = 5f;
                        projectileComponent.trs.gameObject.SetActive (false);
                    }
                }
            }

            // var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();
            // var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();
            // var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();
            // var crosshairPool = ecsSystems.GetWorld ().GetPool<CrosshairComponent> ();
            // var gameData = ecsSystems.GetShared<GameData> ();
            //
            // foreach (var playerEntity in playerFilter) {
            //     ref var playerComponent = ref playerPool.Get (playerEntity);
            //     ref var playerInputComponent = ref playerInputPool.Get (playerEntity);
            //     ref var crosshairComponent = ref crosshairPool.Get (playerEntity);
            //     ref var weaponComponent = ref weaponPool.Get (playerEntity);
            //
            //     
            //     
            //     PointWeaponToCrosshair (ref weaponComponent, ref crosshairComponent);
            //     TryShooting ();
            // }
        }

        private void PointWeaponToCrosshair(ref WeaponComponent weapon, ref CrosshairComponent component) {
            var directon = (component.trs.position - weapon.trs.position).normalized;
            weapon.trs.position = Vector3.MoveTowards (weapon.trs.position, component.trs.position, Time.deltaTime);
            float angle = Mathf.Atan2 (directon.y, directon.x) * Mathf.Rad2Deg;
            weapon.trs.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
        }

        private void TryShooting () {
        }
    }
}