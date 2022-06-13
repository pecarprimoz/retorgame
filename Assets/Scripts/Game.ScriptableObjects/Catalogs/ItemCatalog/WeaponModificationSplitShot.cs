using UnityEngine;

namespace Game.ScriptableObjects.Catalogs.ItemCatalog {
    [CreateAssetMenu (fileName = "Configuration/Split Shot")]
    public class WeaponModificationSplitShot : ItemCatalogEntry {
        public int spawnedProjCount;
    }
}