using Game.ScriptableObjects.Catalogs.EnemyCatalog;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/EnemySpawn")]
    public class GameDirectorConfiguration : ScriptableObject {
        public int id;
        [Header("EnemySpawn")]
        public EnemySpawnConfiguration enemySpawnConfiguration;
        public int spawnCount;
    }
}