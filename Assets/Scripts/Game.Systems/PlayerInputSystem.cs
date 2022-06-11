﻿using Leopotam.EcsLite;
using Game.Components.Player;
using Game.Data;
using UnityEngine;

namespace Game.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerInputComponent>().End();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();
            var gameData = ecsSystems.GetShared<GameData>();

            foreach (var entity in filter)
            {
                ref var playerInputComponent = ref playerInputPool.Get(entity);
                
                playerInputComponent.moveHorizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                playerInputComponent.moveVertical = new Vector3(Input.GetAxisRaw("Vertical"), 0, 0);
                playerInputComponent.mousePos = Input.mousePosition;
                var playerDirection =
                    new Vector2 (playerInputComponent.moveHorizontal.x, playerInputComponent.moveVertical.x);
                playerInputComponent.direction = playerDirection;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    gameData.sceneService.ReloadScene();
                }
            }
        }
    }
}