using System;
using Game.World;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Items.Food {
    public sealed class FoodInitSystem : IEcsInitSystem {
        private readonly EcsWorld ecsWorld = null;
        private readonly GameDefinitions gameDefinitions = null;

        public void Init() {
            if (!gameDefinitions.foodDefinition) throw new Exception($"{nameof(FoodDefinition)} doesn't exists!");

            var foodDefinition = gameDefinitions.foodDefinition;
            var foodObjects = GameObject.FindGameObjectsWithTag("Food");
            foreach (var foodObject in foodObjects) {
                var entity = ecsWorld.NewEntity();
                entity.Replace(new ItemComponent())
                    .Replace(new CreateWorldObjectEvent { transform = foodObject.transform })
                    .Replace(new FoodComponent {
                        scores = foodDefinition.scoresPerFood,
                        speedPenalty = foodDefinition.speedPenalty
                    });
            }

            var energizers = GameObject.FindGameObjectsWithTag("Energizer");
            foreach (var energizer in energizers) {
                var entity = ecsWorld.NewEntity();
                entity.Replace(new EnergizerComponent())
                    .Replace(new ItemComponent())
                    .Replace(new CreateWorldObjectEvent { transform = energizer.transform })
                    .Replace(new FoodComponent {
                        scores = foodDefinition.scoresPerFood * foodDefinition.energizerMultiplier,
                        speedPenalty = foodDefinition.speedPenalty * foodDefinition.energizerMultiplier
                    });
            }
        }
    }
}