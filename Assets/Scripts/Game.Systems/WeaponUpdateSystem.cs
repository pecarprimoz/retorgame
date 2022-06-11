using System;
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Game.Components;
using Game.Components.Player;
using Game.Data;
using UnityEngine;

namespace Game.Systems {
    public class WeaponUpdateSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var playerFilter = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();
            var crosshairPool = ecsSystems.GetWorld ().GetPool<CrosshairComponent> ();
            var gameData = ecsSystems.GetShared<GameData> ();

            foreach (var playerEntity in playerFilter) {
                ref var playerComponent = ref playerPool.Get (playerEntity);
                ref var playerInputComponent = ref playerInputPool.Get (playerEntity);
                ref var crosshairComponent = ref crosshairPool.Get (playerEntity);
                ref var weaponComponent = ref weaponPool.Get (playerEntity);

                
                
                PointWeaponToCrosshair (ref weaponComponent, ref crosshairComponent);
                TryShooting ();
            }
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