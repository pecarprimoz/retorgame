using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/System")]
    public class SystemConfiguration : ScriptableObject {
        public GameConfiguration gameConfig;
        public PlayerConfiguration playerConfig;
        public CanvasConfiguration canvasConfig;
    }
}