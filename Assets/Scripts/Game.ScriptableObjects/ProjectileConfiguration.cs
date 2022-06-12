using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/ProjectileConfiguration")]
    public class ProjectileConfiguration : ScriptableObject {
        public int id;
        public GameObject projectileReference;
        public AnimationCurve curve;
        public float speed;
        public float damage;
        public float lifetime;
        public Vector3 offset;
    }
}