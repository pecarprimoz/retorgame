using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/Canvas configuration")]
    public class CanvasConfiguration : ScriptableObject {
        public GameObject assetReference;
    }
}