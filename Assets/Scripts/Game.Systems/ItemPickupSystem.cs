using System;
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Game.Components;
using Game.Components.Player;
using Game.Data;
using Game.ScriptableObjects.Catalogs.ItemCatalog;
using UnityEngine;

namespace Game.Systems {
    public class ItemPickupSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var playerFilter = ecsSystems.GetWorld ().Filter<PlayerComponent> ().End ();
            var playerPool = ecsSystems.GetWorld ().GetPool<PlayerComponent> ();

            var itemFilter = ecsSystems.GetWorld ().Filter<ItemComponent> ().End ();
            var itemPool = ecsSystems.GetWorld ().GetPool<ItemComponent> ();

            var weaponFilter = ecsSystems.GetWorld ().Filter<WeaponComponent> ().End ();
            var weaponPool = ecsSystems.GetWorld ().GetPool<WeaponComponent> ();

            var gameData = ecsSystems.GetShared<GameData> ();
            foreach (var itemEntity in itemFilter) {
                ref var itemComponent = ref itemPool.Get (itemEntity);
                foreach (var playerEntity in playerFilter) {
                    ref var playerComponent = ref playerPool.Get (playerEntity);
                    if (itemComponent.trs.gameObject.activeInHierarchy &&
                        itemComponent.collider.Distance (playerComponent.collider).isOverlapped) {
                        switch (itemComponent.modificationType) {
                            case ModificationType.Player:
                                break;
                            case ModificationType.Weapon:
                                TryPickupWeaponModification (ref itemComponent, itemComponent.itemId, weaponFilter,
                                    weaponPool,
                                    gameData);
                                break;
                        }
                    }
                }
            }
        }

        private void TryPickupWeaponModification(ref ItemComponent itemComponent, int itemId, EcsFilter weaponFilter,
            EcsPool<WeaponComponent> weaponPool, GameData data) {
            foreach (var weaponEntity in weaponFilter) {
                ref var weaponComponent = ref weaponPool.Get (weaponEntity);
                var weaponConfig = data.gameConfig.itemCatalog.entries.Find (x => x.id == itemId);
                if (weaponConfig is WeaponModificationSplitShot splitShot) {
                    weaponComponent.projectileSpawnCount = splitShot.spawnedProjCount;
                    itemComponent.trs.gameObject.SetActive (false);
                }
            }
        }
    }
}