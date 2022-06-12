using UnityEngine;

namespace Game.Components.Player {
    public struct WeaponComponent {
        public int id;
        public Transform trs;
        public float delayBetweenShots;
        // make new system that will support different offsets before sync transform system
        // just create offset component thats attached to different prefabs, on init read from
        // configuration, dont keep it as property but rather as a component with enitity in some pool that 
        // you will need
        public bool canShoot;
    }
}