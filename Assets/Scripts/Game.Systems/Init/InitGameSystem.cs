using Game.Components;
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
            
            var uiEntity = ecsWorld.NewEntity();
            var uiPool = ecsWorld.GetPool<UiComponent> ();
            uiPool.Add (uiEntity);
            
            ref var cameraComponent = ref cameraPool.Get(cameraEntity);
            ref var uiComponent = ref uiPool.Get(uiEntity);
            
            var camera = Object.Instantiate (gameData.gameSystem.gameConfig.cameraConfiguration.assetReference);
            cameraComponent.camera = camera;
            
            uiComponent.UI = Object.Instantiate (gameData.gameSystem.gameConfig.canvasConfiguration.assetReference);
            uiComponent.camera = camera.GetComponent<Camera> ();
        }
    }
}