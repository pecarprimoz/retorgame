using UnityEngine;

namespace Game.ScriptableObjects.Catalogs.EnemyCatalog {
    [CreateAssetMenu (fileName = "Configuration/EnemySpawn")]
    public class EnemySpawnConfiguration : CatalogEntry {
        private bool active;
        public EnemyConfiguration enemyConfiguration;
        public int minInstanceCount;
        public int maxInstanceCount;
        public float spawnDelay;
    }
}