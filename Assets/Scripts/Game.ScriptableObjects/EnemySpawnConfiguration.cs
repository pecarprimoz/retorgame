using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/EnemySpawn")]
    public class EnemySpawnConfiguration : ScriptableObject {
        public int id;
        private bool active;
        public EnemyConfiguration enemyConfiguration;
        public int minInstanceCount;
        public int maxInstanceCount;
        public float spawnDelay;
    }
}