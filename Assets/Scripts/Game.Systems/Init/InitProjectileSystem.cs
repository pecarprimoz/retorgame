using System.Collections.Generic;
using Game.Components;
using Game.Components.Player;
using Game.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.Init {
    public class InitProjectileSystem : IEcsInitSystem {

        private const int projectileCount = 100;
        public void Init(EcsSystems systems) {
            var ecsWorld = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();
            
            // create projectiles
            for (int i = 0; i < projectileCount; i++) {
                var projectileEntity = ecsWorld.NewEntity();
                var projectile = Object.Instantiate (gameData.gameSystem.playerConfig.weaponConfiguration
                    .projectileConfiguration.projectileReference);
                var projectilePool = ecsWorld.GetPool<ProjectileComponent> ();
                projectilePool.Add (projectileEntity);

                ref var projectileComponent = ref projectilePool.Get (projectileEntity);
                projectileComponent.body = projectile.GetComponent<Rigidbody2D> ();
                projectileComponent.trs = projectile.transform;
                projectileComponent.trs.gameObject.SetActive (false);
            }
            
            foreach (var player in systems.GetWorld ().Filter<PlayerComponent> ().End ()) {
                var weaponPool = ecsWorld.GetPool<WeaponComponent> ();
                weaponPool.Add (player);
            }
            
            
        }
    }
}