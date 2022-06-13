using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects.Catalogs {
    public abstract class CatalogEntry : ScriptableObject {
        public int id;
    }
    
    public abstract class Catalog<T> : ScriptableObject where T : CatalogEntry {
        public List<T> entries;
    }
}