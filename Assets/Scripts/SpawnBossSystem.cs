using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SpawnBossSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts; 

    public SpawnBossSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CurrentBoss, GameMatcher.Time).NoneOf(GameMatcher.GameObject));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.time.value < 0)
            {
                var boss = entity.currentBoss.value;
                var gameField = _contexts.game.gameField;
                var randomPosition = Helper.RandomPosition(gameField.Width, gameField.Height, 5);
                var instance = UnityEngine.Object.Instantiate(boss.Prefab, randomPosition, Quaternion.identity );
                var gameObjectEntityLink = instance.AddComponent<GameObjectEntityLink>();
                gameObjectEntityLink.Entity = entity;
                gameObjectEntityLink.DestroyEntityOnDestroy = false;
                entity.AddGameObject(instance);
                entity.RemoveTime();
            }
        }
    }
}