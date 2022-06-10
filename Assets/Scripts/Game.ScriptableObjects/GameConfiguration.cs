using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/Game")]
    public class GameConfiguration : ScriptableObject {
        public CameraConfiguration cameraConfiguration;
        public CanvasConfiguration canvasConfiguration;
    }
}