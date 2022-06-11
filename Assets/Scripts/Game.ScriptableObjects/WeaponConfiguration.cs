using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/WeaponConfiguration")]
    public class WeaponConfiguration : ScriptableObject {
        public int id;
        public GameObject weaponReference;
        public ProjectileConfiguration projectileConfiguration;
        public float fireRate;
        public Vector3 offset;
    }
}