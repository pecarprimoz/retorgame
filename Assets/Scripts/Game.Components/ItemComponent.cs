using UnityEngine;

namespace Game.Components {
    public struct ItemComponent {
        public int itemId;
        public ModificationType modificationType;
        public Transform trs;
        public Collider2D collider;
    }
}