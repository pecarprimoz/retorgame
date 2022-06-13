using Game.Components;
using Game.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.Init {
    public class InitEnemySpawnSystem : IEcsInitSystem {
        public void Init(EcsSystems systems) {
            var ecsWorld = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();
            var gameDirectionConfig = gameData.gameConfig.gameDirectorConfig;
            var enemyConfiguration = gameData.gameConfig.gameDirectorConfig.enemySpawnConfiguration.enemyConfiguration;
            var enemySpawnConfiguration = gameData.gameConfig.gameDirectorConfig.enemySpawnConfiguration;
            var gameDirectorFilter = systems.GetWorld ().Filter<GameDirectorComponent> ();
            var gameDirectorPool = systems.GetWorld ().GetPool<GameDirectorComponent> ();

            var enemySpawnPool = systems.GetWorld ().GetPool<EnemySpawnComponent> ();
            var enemyPool = systems.GetWorld ().GetPool<EnemyComponent> ();
            
            foreach (var gameDirectorEntity in gameDirectorFilter.End()) {
                ref var gameDirectorComponent = ref gameDirectorPool.Get (gameDirectorEntity);
                for (int i = 0; i < gameDirectorComponent.spawnCount; i++) {
                    var enemySpawnEntity = ecsWorld.NewEntity ();
                    enemySpawnPool.Add (enemySpawnEntity);
                    ref var enemySpawnComponent = ref enemySpawnPool.Get (enemySpawnEntity);
                    enemySpawnComponent.min = gameDirectionConfig.enemySpawnConfiguration.minInstanceCount;
                    enemySpawnComponent.max = gameDirectionConfig.enemySpawnConfiguration.maxInstanceCount ; // change in future
                    enemySpawnComponent.spawnInterval =
                        enemySpawnConfiguration.spawnDelay;
                    enemySpawnComponent.position = Vector3.left;
                    enemySpawnComponent.active = false;
                    var enemyInstanceCount = enemySpawnComponent.max; // game director should drive this via difficulty?
                    // move to int enemy system
                    for (int j = 0; j < enemyInstanceCount; j++) {
                        var enemyEntity = ecsWorld.NewEntity ();
                        enemyPool.Add (enemyEntity);
                        for (int k = 0; k < enemyConfiguration.Count; k++) {
                            var enemyReference = enemyConfiguration[i];
                            var enemy = Object.Instantiate (enemyReference.enemyReference);
                            ref var enemyComponent = ref enemyPool.Get (enemyEntity);
                            enemyComponent.body = enemy.GetComponent<Rigidbody2D> ();
                            enemyComponent.collider = enemy.GetComponent<Collider2D> ();
                            enemyComponent.hp = enemyReference.hp;
                            enemyComponent.speed  = enemyReference.speed;
                            enemyComponent.trs = enemy.transform;
                            enemyComponent.trs.gameObject.SetActive (false);
                        }
                        
                    }
                }
            }
        }
    }
}