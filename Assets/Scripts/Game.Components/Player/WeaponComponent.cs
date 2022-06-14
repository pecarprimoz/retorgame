using UnityEngine;

namespace Game.Components.Player {
    public struct WeaponComponent {
        public int id;
        public Transform trs;
        public Collider2D collider;
        public float delayBetweenShots;
        public bool canShoot;
        public int projectileSpawnCount;
    }
}