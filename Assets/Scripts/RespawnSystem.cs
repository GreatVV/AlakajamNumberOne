using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RespawnSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public RespawnSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Position,
            GameMatcher.GameObject));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var playerDesc = _contexts.game.player.PlayerDescription;
        foreach (var entity in entities)
        {
            var position = entity.position.value;
            var gameObjectValue = entity.gameObject.value;
            if (entity.hasPlayer)
            {
                if (!playerDesc.GameZoneCollider.bounds.Contains(position))
                {
                    gameObjectValue.transform.position = playerDesc.RespawnPosition.position;
                    gameObjectValue.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
            else
            {
                if (!playerDesc.GameZoneCollider.bounds.Contains(position))
                {
                    gameObjectValue.transform.position = Helper.RandomPosition(_contexts.game.gameField.Width,
                        _contexts.game.gameField.Height);
                    gameObjectValue.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }

        }
    }
}