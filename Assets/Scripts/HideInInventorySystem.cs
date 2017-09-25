using System.Collections.Generic;
using Entitas;

public class HideInInventorySystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public HideInInventorySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }


    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.GameObject, GameMatcher.InsideInventory));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasGameObject;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            gameEntity.gameObject.value.SetActive(false);
        }
    }
}