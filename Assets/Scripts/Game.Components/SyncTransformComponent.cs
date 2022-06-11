using UnityEngine;

namespace Game.Components {
    public struct SyncTransformComponent {
        public Transform origin;
        public Transform attach;
        public Vector3 offset;
    }
}