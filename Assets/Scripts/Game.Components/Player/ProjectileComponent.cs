using UnityEngine;

namespace Game.Components.Player {
    public struct ProjectileComponent {
        public int id;
        public Transform trs;
        public Rigidbody2D body;
        public AnimationCurve projectileCurve;
        public Vector3 direction;
        public float damage;
        public float speed;
        public float lifetime;
    }
}