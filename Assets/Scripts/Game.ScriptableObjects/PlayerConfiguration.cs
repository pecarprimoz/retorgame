using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/Player")]
    public class PlayerConfiguration : ScriptableObject {
        public GameObject playerReference;
        public GameObject playerCrosshairReference;
        public float playerSpeed;
    }
}