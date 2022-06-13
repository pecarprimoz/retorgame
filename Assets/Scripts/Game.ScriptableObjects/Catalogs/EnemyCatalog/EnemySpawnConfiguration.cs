using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects.Catalogs.EnemyCatalog {
    [CreateAssetMenu (fileName = "Configuration/EnemySpawn")]
    public class EnemySpawnConfiguration : CatalogEntry {
        private bool active;
        public List<EnemyConfiguration> enemyConfiguration;
        public int minInstanceCount;
        public int maxInstanceCount;
        public float spawnDelay;
    }
}