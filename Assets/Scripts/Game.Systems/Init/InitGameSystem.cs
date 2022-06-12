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
            
            var camera = Object.Instantiate (gameData.gameConfig.uiConfig.cameraConfiguration.assetReference);
            cameraComponent.camera = camera;
            
            uiComponent.UI = Object.Instantiate (gameData.gameConfig.uiConfig.canvasConfiguration.assetReference);
            uiComponent.camera = camera.GetComponent<Camera> ();

            var gameDirectorEntity = ecsWorld.NewEntity ();
            var gameDirectorPool = ecsWorld.GetPool<GameDirectorComponent> ();
            gameDirectorPool.Add (gameDirectorEntity);
            ref var gameDirComp = ref gameDirectorPool.Get (gameDirectorEntity);
            gameDirComp.spawnCount = gameData.gameConfig.gameDirectorConfig.spawnCount;
        }
    }
}