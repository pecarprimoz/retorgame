using Game.Components;
using Game.Components.Player;
using Leopotam.EcsLite;
using Game.Data;
using UnityEngine;

namespace Game.Systems {
    public class EnemyUpdateSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var enemyFilter = ecsSystems.GetWorld ().Filter<EnemyComponent> ().End ();
            var projectileFilter = ecsSystems.GetWorld ().Filter<ProjectileComponent> ().End ();
            var enemyPool = ecsSystems.GetWorld ().GetPool<EnemyComponent> ();
            var projectilePool = ecsSystems.GetWorld ().GetPool<ProjectileComponent> ();
            foreach (var enemyEntity in enemyFilter) {
                ref var enemyComponent = ref enemyPool.Get (enemyEntity);
                if (enemyComponent.trs.gameObject.activeInHierarchy) {
                    foreach (var projectileEntity in projectileFilter) {
                        ref var projectileComponent = ref projectilePool.Get (projectileEntity);
                        if (projectileComponent.trs.gameObject.activeInHierarchy) {
                            if (enemyComponent.collider.Distance (projectileComponent.collider).isOverlapped) {
                                projectileComponent.trs.gameObject.SetActive (false);
                                enemyComponent.trs.gameObject.SetActive (false);
                            }
                        }
                    }
                }
            }
        }
    }
}