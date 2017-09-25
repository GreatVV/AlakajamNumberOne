using System;
using System.Collections.Generic;
using Entitas;

public class ClearFieldSystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> _allItems;
    
    public ClearFieldSystem(Contexts contexts) : base(contexts.game)
    {
        _allItems = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Ingredient));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CurrentBoss);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var items = _allItems.GetEntities();
        foreach (var item in items)
        {
            item.isDestroy = true;
        }
    }
}