using System;
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Game.Components;
using Game.Components.Player;
using Game.Data;
using UnityEngine;

namespace Game.Systems {
    public class PlayerWeaponUpdateSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var playerFilter = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var weaponFilter = ecsSystems.GetWorld ().Filter<WeaponComponent> ().End ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();
            var crosshairPool = ecsSystems.GetWorld ().GetPool<CrosshairComponent> ();
            var gameData = ecsSystems.GetShared<GameData> ();

            foreach (var weaponEntity in weaponFilter) {
                ref var weaponComponent = ref weaponPool.Get (weaponEntity);
                if (!weaponComponent.canShoot) {
                    weaponComponent.delayBetweenShots -= Time.deltaTime;
                }

                if (weaponComponent.delayBetweenShots <= 0) {
                    weaponComponent.delayBetweenShots = gameData.gameGame.playerConfig.weaponConfiguration.fireRate;
                    weaponComponent.canShoot = true;
                }
                
                foreach (var playerEntity in playerFilter) {
                    ref var playerInputComponent = ref playerInputPool.Get (playerEntity);
                    ref var crosshairComponent = ref crosshairPool.Get (playerEntity);
                    PointWeaponToCrosshair (ref weaponComponent, ref crosshairComponent, ref playerInputComponent);
                }
            }
            
        }

        private void PointWeaponToCrosshair(ref WeaponComponent weapon, ref CrosshairComponent crosshair, ref PlayerInputComponent input) {
            var direction = (crosshair.trs.position - weapon.trs.position).normalized;
            input.lookDirection = direction;
            weapon.trs.position = Vector3.MoveTowards (weapon.trs.position, crosshair.trs.position, Time.deltaTime * 15);
            float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
            weapon.trs.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
        }

    }
}