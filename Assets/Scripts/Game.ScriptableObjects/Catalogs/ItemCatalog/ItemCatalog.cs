using UnityEngine;

namespace UnityEngine {
    public enum ModificationType {
        Player,
        Weapon
    }
}

namespace Game.ScriptableObjects.Catalogs.ItemCatalog {
    [CreateAssetMenu (fileName = "Configuration/Item Catalog")]
    public class ItemCatalog : Catalog<ItemCatalogEntry> {
    }
    [CreateAssetMenu (fileName = "Configuration/Item Catalog Entry")]
    public class ItemCatalogEntry : CatalogEntry {
        public ModificationType modificationType;
        public GameObject itemReference;
    }
}