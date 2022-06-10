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
    [SerializeField]
    private CanvasConfiguration uiConfig;

    void Start () {
        ecsWorld = new EcsWorld ();
        var gameData = new GameData
        {
            gameSystem = gameConfig,
            UI =  uiConfig,
            sceneService = new SceneService (),
        };

        initSystems = new EcsSystems (ecsWorld, gameData)
            .Add (new InitCameraSystem ())
            .Add (new InitPlayerSystem ());

        initSystems.Init ();

        updateSystems = new EcsSystems (ecsWorld, gameData)
            .Add (new PlayerInputSystem ())
            .Add (new PlayerAnimationSystem ())
#if UNITY_EDITOR
            .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ());
#endif
        updateSystems.Init ();

        fixedUpdateSystems = new EcsSystems (ecsWorld, gameData)
            .Add (new PlayerMoveSystem ())
            .Add (new CameraSyncSystem ());

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