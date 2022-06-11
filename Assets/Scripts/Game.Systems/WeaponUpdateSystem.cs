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
            var gameData = ecsSystems.GetShared<GameData>();

            foreach (var playerEntity in playerFilter) {
                ref var playerComponent = ref playerPool.Get (playerEntity);
                ref var playerInputComponent = ref playerInputPool.Get (playerEntity);
                ref var weaponComponent = ref weaponPool.Get (playerEntity);
                /*
                 * VERTICAL    (DOWN UP)
                 *    vector.x  -1    1
                 * HORIZONTAL (LEFT RIGHT)
                 *    vector.x  -1    1
                 */
                if (playerInputComponent.IsMoving) {
                    playerComponent.body.AddForce (playerInputComponent.direction * playerComponent.speed,
                        ForceMode2D.Force);
                }
                else {
                    playerComponent.body.velocity = Vector2.zero;
                    playerComponent.body.angularVelocity = 0;
                    playerComponent.body.Sleep();
                }

            }
        }
    }
}