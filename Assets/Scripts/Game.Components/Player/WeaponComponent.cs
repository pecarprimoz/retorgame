using UnityEngine;

namespace Game.Components.Player {
    public struct WeaponComponent {
        public int id;
        public Transform trs;
        public float delayBetweenShots;
        public Vector3 offset;
        public bool canShoot;
    }
}