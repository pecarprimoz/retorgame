using Game.Components;
using Game.Components.Player;
using Game.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.Init {
    public class InitWeaponSystem : IEcsInitSystem {
        public void Init(EcsSystems systems) {
            var ecsWorld = systems.GetWorld ();
            var gameData = systems.GetShared<GameData> ();
            // add new player entity to player pool
            var offsetPool = ecsWorld.GetPool<OffsetComponent> ();
            var weaponEntity = ecsWorld.NewEntity ();
            var weaponPool = ecsWorld.GetPool<WeaponComponent> ();
            weaponPool.Add (weaponEntity);
            var weapon = Object.Instantiate (gameData.gameConfig.playerConfig.weaponConfiguration.weaponReference);
            ref var weaponComponent = ref weaponPool.Get (weaponEntity);
            weaponComponent.trs = weapon.transform;
            weaponComponent.projectileSpawnCount = gameData.gameConfig.playerConfig.weaponConfiguration.spawnedProjCount;

            var syncTRSPool = ecsWorld.GetPool<SyncTransformComponent> ();
            syncTRSPool.Add (weaponEntity);
            offsetPool.Add (weaponEntity);
            
            var playerFilter = ecsWorld.Filter<PlayerComponent> ().End ();
            var playerPool = ecsWorld.GetPool<PlayerComponent> ();
            foreach (var playerEntity in playerFilter) {
                ref var playerComponent = ref playerPool.Get (playerEntity);

                ref var syncTRS = ref syncTRSPool.Get (weaponEntity);
                syncTRS.attach = playerComponent.trs;
                syncTRS.origin = weaponComponent.trs;
                ref var offsetComponent = ref offsetPool.Get (weaponEntity);
                offsetComponent.offset = gameData.gameConfig.playerConfig.weaponConfiguration.offset;
            }

            var spriteDirectionPool = ecsWorld.GetPool<SpriteDirectionComponent> ();
            spriteDirectionPool.Add (weaponEntity);
            ref var dirWepComp = ref spriteDirectionPool.Get (weaponEntity);
            dirWepComp.spriteTRS = weapon.transform;
        }
    }
}