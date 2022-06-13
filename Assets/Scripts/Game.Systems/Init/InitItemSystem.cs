using Game.Components;
using Game.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.Init {
    public class InitItemSystem : IEcsInitSystem {
        public void Init(EcsSystems systems) {
            var ecsWorld = systems.GetWorld ();
            var gameData = systems.GetShared<GameData> ();

            var itemPool = ecsWorld.GetPool<ItemComponent> ();
            foreach (var itemEntry in gameData.gameConfig.itemCatalog.entries) {
                var itemEntity = ecsWorld.NewEntity ();
                itemPool.Add (itemEntity);
                ref var itemComponent = ref itemPool.Get (itemEntity);
                var item = Object.Instantiate (itemEntry.itemReference);
                itemComponent.itemId = itemEntry.id;
                itemComponent.trs = item.transform;
                itemComponent.collider = item.gameObject.GetComponent<Collider2D> ();
                itemComponent.trs.gameObject.SetActive (false);
            }
        }
    }
}