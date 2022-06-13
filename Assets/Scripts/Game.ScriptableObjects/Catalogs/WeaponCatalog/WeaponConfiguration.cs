using Game.ScriptableObjects.Catalogs.ProjectileCatalog;
using UnityEngine;

namespace Game.ScriptableObjects.Catalogs.WeaponCatalog {
    [CreateAssetMenu (fileName = "Configuration/WeaponConfiguration")]
    public class WeaponConfiguration : CatalogEntry {
        public GameObject weaponReference;
        public ProjectileConfiguration projectileConfiguration;
        public Vector3 offset;
        
        // weapon modifications        
        public float fireRate;
        public int spawnedProjCount;
    }
}