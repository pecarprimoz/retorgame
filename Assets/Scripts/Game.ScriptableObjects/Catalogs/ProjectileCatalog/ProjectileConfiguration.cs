using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects.Catalogs.ProjectileCatalog {
    [CreateAssetMenu (fileName = "Configuration/ProjectileConfiguration")]
    public class ProjectileConfiguration : CatalogEntry {
        public GameObject projectileReference;
        public AnimationCurve curve;
        public float speed;
        public float damage;
        public float lifetime;
        public Vector3 offset;
    }
}