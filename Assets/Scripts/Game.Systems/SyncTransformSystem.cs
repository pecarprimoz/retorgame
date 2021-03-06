using Game.Components;
using Leopotam.EcsLite;

namespace Game.Systems {
    // this is kinda the same as sync trs
    public class SyncTransformSystem : IEcsRunSystem {
        public void Run(EcsSystems ecsSystems) {
            var syncTRSFilter = ecsSystems.GetWorld ().Filter<SyncTransformComponent> ().End ();
            var syncTRSPool = ecsSystems.GetWorld ().GetPool<SyncTransformComponent> ();
            var offsetPool = ecsSystems.GetWorld ().GetPool<OffsetComponent> ();
            foreach (var trsEntity in syncTRSFilter) {
                ref var camComponent = ref syncTRSPool.Get (trsEntity);
                ref var offsetComponent = ref offsetPool.Get (trsEntity);
                camComponent.origin.position = camComponent.attach.position + offsetComponent.offset;
            }
        }
    }
}