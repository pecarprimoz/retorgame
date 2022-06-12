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
                var projectile = Object.Instantiate (gameData.gameConfig.playerConfig.weaponConfiguration
                    .projectileConfiguration.projectileReference);
                var projectilePool = ecsWorld.GetPool<ProjectileComponent> ();
                var spriteDirectionPool = ecsWorld.GetPool<SpriteDirectionComponent> ();
                projectilePool.Add (projectileEntity);
                spriteDirectionPool.Add (projectileEntity);
                
                ref var projectileComponent = ref projectilePool.Get (projectileEntity);
                projectileComponent.body = projectile.GetComponent<Rigidbody2D> ();
                projectileComponent.trs = projectile.transform;
                projectileComponent.lifetime = gameData.gameConfig.playerConfig.weaponConfiguration
                    .projectileConfiguration.lifetime;
                projectileComponent.speed = gameData.gameConfig.playerConfig.weaponConfiguration
                    .projectileConfiguration.speed;
                projectileComponent.damage = gameData.gameConfig.playerConfig.weaponConfiguration
                    .projectileConfiguration.damage;
                
                // do this rather with a transform reference on weapon, from there then spawn the projectile
                // offsetComponent.offset = gameData.gameConfig.playerConfig.weaponConfiguration
                //     .projectileConfiguration.offset;
                projectileComponent.trs.gameObject.SetActive (false);
            }
            
        }
    }
}