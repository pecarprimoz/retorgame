using System;
using Game.Data;
using Game.ScriptableObjects;
using Game.Services;
using Game.Systems;
using Game.Systems.Init;
using Leopotam.EcsLite;
using UnityEngine;

sealed class EcsStartup : MonoBehaviour {
    [Header ("ECS")] private EcsWorld ecsWorld;
    private EcsSystems initSystems;
    private EcsSystems updateSystems;
    private EcsSystems fixedUpdateSystems;

    [Header ("SO References")] [SerializeField]
    private SystemConfiguration gameConfig;

    void Start () {
        ecsWorld = new EcsWorld ();
        var gameData = new GameData
        {
            gameSystem = gameConfig,
            sceneService = new SceneService (),
            runetimeData = new RuntimeData (),
        };

        initSystems = new EcsSystems (ecsWorld, gameData)
            .Add (new InitGameSystem ())
            .Add (new InitPlayerSystem ())
            .Add (new InitWeaponSystem ())
            .Add (new InitProjectileSystem ());

        initSystems.Init ();

        updateSystems = new EcsSystems (ecsWorld, gameData)
            .Add (new PlayerInputSystem ())
            .Add (new PlayerAnimationSystem ())
#if UNITY_EDITOR
            .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ());
#endif
        updateSystems.Init ();

        fixedUpdateSystems = new EcsSystems (ecsWorld, gameData)
            .Add (new CameraSyncSystem ())
            .Add (new PlayerMoveSystem ())
            .Add (new SyncTransformSystem())
            .Add (new WeaponUpdateSystem ())
            .Add (new ProjectileSystem ());

        fixedUpdateSystems.Init ();
    }

    void Update () {
        updateSystems?.Run ();
    }

    private void FixedUpdate () {
        fixedUpdateSystems.Run ();
    }

    void OnDestroy () {
        if (updateSystems != null) {
            updateSystems.Destroy ();
            initSystems.Destroy ();
            // add here cleanup for custom worlds, for example:
            // _systems.GetWorld ("events").Destroy ();
            updateSystems.GetWorld ().Destroy ();
            initSystems.GetWorld ().Destroy ();
            updateSystems = null;
        }
    }
}