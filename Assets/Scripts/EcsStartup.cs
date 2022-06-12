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
    private GameConfiguration gameConfig;

    void Start () {
        ecsWorld = new EcsWorld ();
        var gameData = new GameData
        {
            gameConfig = gameConfig,
            sceneService = new SceneService (),
        };

        initSystems = new EcsSystems (ecsWorld, gameData)
            .Add (new InitGameSystem ())
            .Add (new InitPlayerSystem ())
            .Add (new InitWeaponSystem ())
            .Add (new InitProjectileSystem ())
            .Add (new InitEnemySpawnSystem ())
            .Add (new InitEnemySystem ());

        initSystems.Init ();

        updateSystems = new EcsSystems (ecsWorld, gameData)
            .Add (new PlayerInputSystem ())
            .Add (new PlayerAnimationSystem ())
            .Add (new CameraSyncSystem ())
            .Add (new EnemyUpdateSystem ())
            .Add (new PlayerWeaponUpdateSystem ())
            .Add (new PlayerShootingSystem ())
            .Add (new ProjectileSystem ())
            .Add (new EnemySpawnSystem ())
            .Add (new SpriteDirectionSystem ())
            .Add (new SyncTransformSystem())

#if UNITY_EDITOR
            .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ());
#endif
        updateSystems.Init ();

        fixedUpdateSystems = new EcsSystems (ecsWorld, gameData)
            .Add (new PlayerMoveSystem ());

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