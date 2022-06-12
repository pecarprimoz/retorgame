using Game.Components;
using Game.Components.Player;
using Game.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.Init {
    public class InitEnemySystem : IEcsInitSystem {
        public void Init(EcsSystems systems) {
            var ecsWorld = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();
        }
    }
}