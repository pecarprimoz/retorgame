using UnityEngine;

namespace Game.Components {
    public struct EnemySpawnComponent {
        public bool active;
        public Vector3 position;
        public int min;
        public int max;
        public float spawnInterval;
    }
}