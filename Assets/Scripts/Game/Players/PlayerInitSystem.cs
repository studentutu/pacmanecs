﻿using System;
using Utils;
using Game.Moving;
using Game.World;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Players {
    public sealed class PlayerInitSystem : IEcsInitSystem {
        private readonly EcsWorld ecsWorld = null;
        private readonly GameDefinitions gameDefinitions = null;

        public void Init() {
            if (!gameDefinitions.playerDefinition) throw new Exception($"{nameof(PlayerDefinition)} doesn't exists!");

            var playerCount = 0;
            var playerDefinition = gameDefinitions.playerDefinition;
            var playerObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in playerObjects) {
                var startPosition = player.transform.position.ToVector2Int();
                var startHeading = playerCount % 2 != 0
                    ? Directions.Right
                    : Directions.Left;

                var playerEntity = ecsWorld.NewEntity();
                playerEntity.Replace(new CreateWorldObjectEvent { transform = player.transform })
                    .Replace(new PlayerComponent {
                        lives = playerDefinition.startLives,
                        num = ++playerCount,
                        spawnPosition = startPosition,
                    })
                    .Replace(new MoveComponent {
                        heading = startHeading,
                        desiredPosition = startPosition,
                        speed = playerDefinition.startSpeed,
                    });
            }
        }
    }
}