using System.Collections.Generic;
using Entitas;

public class UpdateHealthBarSystem : ReactiveSystem<GameEntity>
{   
    private Contexts _contexts;

    public UpdateHealthBarSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Health, GameMatcher.MaxHealth,
            GameMatcher.HealthBarBehaviour));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var healthBar = entity.healthBarBehaviour.value;
            healthBar.Slider.normalizedValue = entity.health.value / entity.maxHealth.value;
        }
    }
}