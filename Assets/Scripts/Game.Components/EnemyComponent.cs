using UnityEngine;

namespace Game.Components {
    public struct EnemyComponent {
        public int hp;
        public float speed;
        public Transform trs;
        public Collider2D collider;
        public Rigidbody2D body;
    }
}