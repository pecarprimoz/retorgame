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
            var playerFilter = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var weaponFilter = ecsSystems.GetWorld ().Filter<WeaponComponent> ().End ();
            var enemyPool = ecsSystems.GetWorld ().GetPool<EnemyComponent> ();
            var projectilePool = ecsSystems.GetWorld ().GetPool<ProjectileComponent> ();
            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();
            foreach (var enemyEntity in enemyFilter) {
                ref var enemyComponent = ref enemyPool.Get (enemyEntity);
                MoveEnemyToPlayer (ref enemyComponent, playerFilter, playerPool);
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

                    foreach (var weaponEntity in weaponFilter) {
                        ref var weaponComponent = ref weaponPool.Get (weaponEntity);
                        if (weaponComponent.collider.Distance (enemyComponent.collider).isOverlapped) {
                            enemyComponent.trs.gameObject.SetActive (false);                            
                        }
                    }
                }
            }
        }

        private void MoveEnemyToPlayer (ref EnemyComponent enemyComponent, EcsFilter playerFilter,
            EcsPool<PlayerComponent> playerPool) {
            foreach (var playerEntity in playerFilter) {
                ref var playerComponent = ref playerPool.Get (playerEntity);
                var direction = (playerComponent.trs.position - enemyComponent.trs.position).normalized;
                enemyComponent.trs.position = Vector3.MoveTowards (enemyComponent.trs.position,
                    playerComponent.trs.position, Time.deltaTime * enemyComponent.speed);
                float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
                enemyComponent.trs.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
            }

        }
    }
}