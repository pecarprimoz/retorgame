using Game.Components;
using Leopotam.EcsLite;
using Game.Data;
using UnityEngine;

namespace Game.Systems {
    public class EnemySpawnSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var gameData = ecsSystems.GetShared<GameData>();
            var enemySpawnFilter = ecsSystems.GetWorld ().Filter<EnemySpawnComponent> ().End ();
            var enemyFilter = ecsSystems.GetWorld ().Filter<EnemyComponent> ().End ();
            var spawnPool = ecsSystems.GetWorld ().GetPool<EnemySpawnComponent> ();
            var enemyPool = ecsSystems.GetWorld ().GetPool<EnemyComponent> ();

            foreach (var spawnEntity in enemySpawnFilter) {
                ref var spawnComponent = ref spawnPool.Get (spawnEntity);
                if (spawnComponent.spawnInterval <= 0) {
                    spawnComponent.active = false;
                    spawnComponent.spawnInterval =
                        gameData.gameConfig.gameDirectorConfig.enemySpawnConfiguration.spawnDelay;
                }

                if (!spawnComponent.active) {
                    // enemy spawn 
                    foreach (var enemyEntity in enemyFilter) {
                        ref var enemyComponent = ref enemyPool.Get (enemyEntity);
                        if (!enemyComponent.trs.transform.gameObject.activeInHierarchy) {
                            enemyComponent.trs.position = spawnComponent.position;
                            enemyComponent.trs.gameObject.SetActive (true);
                        }
                    }
                    spawnComponent.active = true;
                }
                else {
                    // enemy spawn is active
                    spawnComponent.spawnInterval -= Time.deltaTime;
                }
            }
        }
    }
}