    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    namespace Game.ScriptableObjects {
        [CreateAssetMenu (fileName = "Configuration/Camera configuration")]
        public class CameraConfiguration : ScriptableObject {
            public GameObject assetReference;
        }
    }