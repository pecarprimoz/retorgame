using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/System")]
    public class GameConfiguration : ScriptableObject {
        public UIConfiguration uiConfig;
        public PlayerConfiguration playerConfig;
        public GameDirectorConfiguration gameDirectorConfig;
    }
}