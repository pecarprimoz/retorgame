using UnityEngine;

namespace Game.Components.Player {
    public struct PlayerComponent {
        public int hp;
        public float speed;
        public Transform trs;
        public Rigidbody2D body;
    }
}