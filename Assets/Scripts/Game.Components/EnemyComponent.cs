using UnityEngine;

namespace Game.Components {
    public struct EnemyComponent {
        public int hp;
        public float speed;
        public Transform trs;
        public Rigidbody2D body;
    }
}