using UnityEngine;

namespace Game.ScriptableObjects.Catalogs.ItemCatalog {
    [CreateAssetMenu (fileName = "Configuration/Speed Up")]
    public class PlayerModificationSpeedUp : ItemCatalogEntry {
        public int speedUpAmmount;
    }
}