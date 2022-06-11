using UnityEngine;

namespace Game.Components.Player {
    public struct PlayerInputComponent {
        public Vector3 moveVertical;
        public Vector3 moveHorizontal;
        public Vector3 mousePos;
        public Vector3 direction;

        public bool IsMoving => direction.x != 0 || direction.y != 0;
    }
}