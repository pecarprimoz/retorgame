using Game.Components;
using Game.Components.Player;
using Game.Data;
using Game.ScriptableObjects;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.Init {
    public class InitCameraSystem : IEcsInitSystem {
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
        }
    }
}