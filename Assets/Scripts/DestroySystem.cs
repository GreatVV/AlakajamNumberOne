using System.Collections.Generic;
using Entitas;
using Entitas.VisualDebugging.Unity;

public class DestroySystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public DestroySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Destroy);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            if (gameEntity.hasGameObject && gameEntity.gameObject.value)
            {
                gameEntity.gameObject.value.DestroyGameObject();
            }
            gameEntity.Destroy();
        }
    }
}