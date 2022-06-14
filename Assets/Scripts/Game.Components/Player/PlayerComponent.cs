using UnityEngine;

namespace Game.Components.Player {
    public struct PlayerComponent {
        public int hp;
        public float speed;
        public float dashSpeed;
        public float dashCooldown;
        public bool canDash;
        public Transform trs;
        public Rigidbody2D body;
        public Collider2D collider;
        
        public ParticleSystem particleSys;

    }
}