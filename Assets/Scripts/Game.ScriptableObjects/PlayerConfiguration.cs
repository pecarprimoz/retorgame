using Game.ScriptableObjects.Catalogs.WeaponCatalog;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/Player")]
    public class PlayerConfiguration : ScriptableObject {
        public GameObject playerReference;
        public GameObject playerCrosshairReference;
        public WeaponConfiguration weaponConfiguration;
        public float playerSpeed;
        public float dashSpeed;
        public float dashCooldown;
    }
}