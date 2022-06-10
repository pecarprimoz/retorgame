using Game.Components;
using Game.Components.Player;
using Game.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.Init {
    public class InitGameSystem : IEcsInitSystem {
        public void Init(EcsSystems systems) {
            var ecsWorld = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();

            var cameraEntity = ecsWorld.NewEntity();
            var cameraPool = ecsWorld.GetPool<CameraComponent>();
            cameraPool.Add(cameraEntity);
            
            ref var cameraComponent = ref cameraPool.Get(cameraEntity);
            
            var camera = Object.Instantiate (gameData.gameSystem.gameConfig.cameraConfiguration.assetReference);
            cameraComponent.camera = camera;
        }
    }
}