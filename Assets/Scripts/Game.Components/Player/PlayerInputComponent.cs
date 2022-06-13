using UnityEngine;

namespace Game.Components.Player {
    public struct PlayerInputComponent {
        public Vector3 moveVertical;
        public Vector3 moveHorizontal;
        public Vector3 mousePos;
        public Vector3 movementDirection;
        public Vector3 lookDirection;

        public bool mouse0;
        public bool mouse1;

        public bool IsMoving => movementDirection.x != 0 || movementDirection.y != 0;
    }
}