using UnityEngine;

namespace Game.Components.Player {
    public struct WeaponComponent {
        public int id;
        public Transform trs;
        public float fireRate;
        public Vector3 offset;
    }
}