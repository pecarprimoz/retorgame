using Game.Components;
using Leopotam.EcsLite;
using Game.Components.Player;
using UnityEngine;

namespace Game.Systems {
    // this is kinda the same as sync trs
    public class CameraSyncSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var filterCamera = ecsSystems.GetWorld ().Filter<CameraComponent> ().End ();
            var filterPlayer = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var syncCamPool = ecsSystems.GetWorld ().GetPool<CameraComponent> ();
            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();

            foreach (var cameraEntity in filterCamera) {
                ref var camComponent = ref syncCamPool.Get (cameraEntity);
                foreach (var playerEntity in filterPlayer) {
                    if(!playerPool.Has (playerEntity)) continue;
                    ref var playerComponent = ref playerPool.Get (playerEntity);
                    var playerPosition = playerComponent.trs.position;
                    var playerInWorldPosition = new Vector3 (playerPosition.x, playerPosition.y,
                        camComponent.camera.transform.position.z);
                    camComponent.camera.transform.position = playerInWorldPosition;
                    camComponent.offset = playerComponent.trs.position -
                                          camComponent.camera.transform
                                              .position;
                }
            }
        }
    }
}