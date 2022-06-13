using System.Collections;
using System.Collections.Generic;
using Game.ScriptableObjects.Catalogs;
using Game.ScriptableObjects.Catalogs.ItemCatalog;
using UnityEngine;

namespace Game.ScriptableObjects {
    [CreateAssetMenu (fileName = "Configuration/System")]
    public class GameConfiguration : ScriptableObject {
        public UIConfiguration uiConfig;
        public PlayerConfiguration playerConfig;
        public GameDirectorConfiguration gameDirectorConfig;
        public Catalog<ItemCatalogEntry> itemCatalog;
    }
}