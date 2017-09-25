using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SpawnEffectOnHitSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public SpawnEffectOnHitSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Projectile, GameMatcher.GameObject, GameMatcher.Destroy));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var effectManager = _contexts.game.effectManager.value;
        foreach (var gameEntity in entities)
        {
            if (gameEntity.hasUsage && gameEntity.usage.value == UsageType.Weapon)
            {
                var gameObject = gameEntity.gameObject.value;
                var prefab = effectManager.HitEffectPrefab;
                var effect = UnityEngine.Object.Instantiate(prefab, gameObject.transform.position,
                    gameObject.transform.localRotation);
                Object.Destroy(effect, 2);
            }
        }
    }
}