using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Game.Components;
using Game.Components.Player;
using Game.Data;
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

            var gameData = ecsSystems.GetShared<GameData>();

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
                    playerComponent.body.velocity =
                        (Vector2) playerInputComponent.movementDirection * playerComponent.speed;
                }
                else {
                    playerComponent.body.velocity = Vector2.zero;
                    playerComponent.body.angularVelocity = 0;
                    playerComponent.body.Sleep ();
                }

                if (playerInputComponent.mouse1 && playerComponent.canDash) {
                    playerComponent.body.AddForce (playerInputComponent.lookDirection * playerComponent.dashSpeed,
                        ForceMode2D.Impulse);
                    playerComponent.particleSys.Play ();
                    playerComponent.canDash = false;
                    playerComponent.dashCooldown = gameData.gameConfig.playerConfig.dashCooldown;
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

                if (!playerComponent.canDash) {
                    playerComponent.dashCooldown -= Time.deltaTime;
                    if (playerComponent.dashCooldown <= 0) {
                        playerComponent.canDash = true;
                    }
                }
            }
        }
    }
}