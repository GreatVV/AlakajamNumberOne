using System;
using System.Collections.Generic;
using Entitas;

public class GenerateItemsSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public GenerateItemsSystem(Contexts contexts):base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.GenerationDescription, GameMatcher.GameField,
            GameMatcher.NeedRegenerate));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var entity = entities.SingleEntity();
        var description = entity.generationDescription.value;
        var gameField = entity.gameField;

        foreach (var item in description.Ingredients)
        {
            for (int i = 0; i < item.Amount; i++)
            {
                var ingredient = _contexts.game.CreateEntity();
                ingredient.AddIngredient(item.Type);
                ingredient.AddColor(item.Color);
                ingredient.AddUsage(UsageType.Craft);
            }
        }
    }
}