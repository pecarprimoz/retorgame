using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/Enemy")]
    public class EnemyConfiguration : ScriptableObject {
        public GameObject enemyReference;
        public WeaponConfiguration weaponConfiguration;
        public float speed;
        public int hp;
    }
}