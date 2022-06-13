using Game.ScriptableObjects.Catalogs.WeaponCatalog; // is  this bad
using UnityEngine;

namespace Game.ScriptableObjects.Catalogs.EnemyCatalog {
    [CreateAssetMenu (fileName = "Configuration/Enemy")]
    public class EnemyConfiguration : CatalogEntry {
        public GameObject enemyReference;
        public WeaponConfiguration weaponConfiguration;
        public float speed;
        public int hp;
    }
}