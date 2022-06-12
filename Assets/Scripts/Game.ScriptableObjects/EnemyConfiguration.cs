using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/Enemy")]
    public class EnemyConfiguration : ScriptableObject {
        public GameObject enemyReference;
        public WeaponConfiguration weaponConfiguration;
        public float enemySpeed;
        public float enemyHp;
    }
}