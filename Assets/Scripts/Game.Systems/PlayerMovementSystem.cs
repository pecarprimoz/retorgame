using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Game.Components;
using Game.Components.Player;
using UnityEngine;

namespace Game.Systems {
    public class PlayerMoveSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var playerFilter = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var cameraFilter = ecsSystems.GetWorld ().Filter<CameraComponent> ().End ();

            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();
            var cameraPool = ecsSystems.GetWorld ().GetPool<CameraComponent> ();
            var crosshairPool = ecsSystems.GetWorld ().GetPool<CrosshairComponent> ();
            var playerInputPool = ecsSystems.GetWorld ().GetPool<PlayerInputComponent> ();

            foreach (var playerEntity in playerFilter) {
                ref var playerComponent = ref playerPool.Get (playerEntity);
                ref var playerInputComponent = ref playerInputPool.Get (playerEntity);
                ref var crosshairComponent = ref crosshairPool.Get (playerEntity);
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
                    // playerComponent.body.velocity = Vector2.zero;
                    // playerComponent.body.angularVelocity = 0;
                    // playerComponent.body.Sleep();
                }

                // handle crosshair logic&view
                foreach (var _ in cameraFilter) {
                    ref var cameraComponent = ref cameraPool.Get (_);
                    var gameViewMousePos = cameraComponent.camera.GetComponent<Camera> ()
                        .ScreenToWorldPoint (playerInputComponent.mousePos) + cameraComponent.offset;
                    var endCrosshairPosition = new Vector3 (gameViewMousePos.x, gameViewMousePos.y,
                        crosshairComponent.trs.position.z);
                    crosshairComponent.trs.position = endCrosshairPosition;
                }
            }
        }
    }
}