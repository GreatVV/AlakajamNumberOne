using System.Collections.Generic;
using Entitas;

public class UpdateChoseItemUISystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    
    public UpdateChoseItemUISystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CurrentProjectile.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (_contexts.game.playerEntity.hasCurrentProjectile && _contexts.game.playerEntity.currentProjectile.value != null)
        {
            var projectile = _contexts.game.playerEntity.currentProjectile.value;
            projectile.inventoryIcon.value.IsChosen(true);
        }
        else
        {
            foreach (var inventoryIcon in _contexts.game.iconCache.value)
            {
                inventoryIcon.Value.IsChosen(false);
            }
        }
    }
}