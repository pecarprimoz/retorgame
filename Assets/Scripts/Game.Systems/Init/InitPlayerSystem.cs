using Game.Components;
using Game.Components.Player;
using Game.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.Init {
    public class InitPlayerSystem : IEcsInitSystem {
        public void Init(EcsSystems systems) {
            var ecsWorld = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();
            // add new player entity to player pool
            var playerEntity = ecsWorld.NewEntity();
            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            playerPool.Add(playerEntity);
            
            // add player entity to input pool
            var playerInputPool = ecsWorld.GetPool<PlayerInputComponent>();
            playerInputPool.Add(playerEntity);
            
            var crosshairPool = ecsWorld.GetPool<CrosshairComponent> ();
            crosshairPool.Add (playerEntity);
            
            ref var playerComponent = ref playerPool.Get(playerEntity);
            // upgrade this to a catalog based system down the road, its bad config also has references to assets
            // you could do this in a separate catalog entry that just holds and ID, link that ID to config, then access
            // a common catalog system / data asset 
            var player = Object.Instantiate (gameData.gameSystem.playerConfig.playerReference);
            playerComponent.trs = player.transform;
            playerComponent.speed = gameData.gameSystem.playerConfig.playerSpeed;
            playerComponent.body = player.GetComponent<Rigidbody2D> ();
            
            var uiPool = ecsWorld.GetPool<UiComponent> ();
            ref var crosshairComponent = ref crosshairPool.Get(playerEntity);
            // second way to do it, is it bad ? i like it
            foreach (var _ in systems.GetWorld ().Filter<UiComponent> ().End ()) {
                ref var ui = ref uiPool.Get(_);
                crosshairComponent.trs = ui.UI.GetComponent<UiWorldView> ().crosshair.transform;
            }
        }
    }
}