using Game.Components;
using Game.Components.Player;
using Game.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.Init {
    public class InitWeaponSystem : IEcsInitSystem {
        public void Init(EcsSystems systems) {
            var ecsWorld = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();
            // add new player entity to player pool
            foreach (var player in systems.GetWorld ().Filter<PlayerComponent> ().End ()) {
                var weaponEntity = ecsWorld.NewEntity ();
                var weaponPool = ecsWorld.GetPool<WeaponComponent> ();
                weaponPool.Add (player);
                var weapon = Object.Instantiate (gameData.gameSystem.playerConfig.weaponConfiguration.weaponReference);
                ref var weaponComponent = ref weaponPool.Get(player);
                weaponComponent.trs = weapon.transform;
                
                var syncEntity = ecsWorld.NewEntity();
                var syncTRSPool = ecsWorld.GetPool<SyncTransformComponent> ();
                syncTRSPool.Add (syncEntity);
                
                var playerPool = ecsWorld.GetPool<PlayerComponent> ();
                ref var playerComponent = ref playerPool.Get(player);

                ref var syncTRS = ref syncTRSPool.Get(syncEntity);
                syncTRS.attach = playerComponent.trs;
                syncTRS.origin = weaponComponent.trs;
                syncTRS.offset = gameData.gameSystem.playerConfig.weaponConfiguration.offset;
                
                var spriteDirectionPool = ecsWorld.GetPool<SpriteDirectionComponent> ();

                spriteDirectionPool.Add (weaponEntity);
                ref var dirWepComp = ref spriteDirectionPool.Get (weaponEntity);
                dirWepComp.spriteTRS = weapon.transform;
            }
            
            
        }
    }
}