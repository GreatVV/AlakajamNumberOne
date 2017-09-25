using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class UpdateWorkbenchRecipeSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public UpdateWorkbenchRecipeSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Recipe, GameMatcher.CurrentBoss));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var workBench = _contexts.game.workbench.value;
        var recipe = entities.SingleEntity().recipe.value;
        var itemFactory = _contexts.game.ingredientViewFactory.value;
        for (var index = 0; index < workBench.Icons.Length; index++)
        {
            var workBenchIcon = workBench.Icons[index];
            if (index < recipe.Ingredients.Count)
            {
                var requiredIngredientDesc = recipe.Ingredients[index];
                var icon = itemFactory.ById(requiredIngredientDesc.Type, requiredIngredientDesc.Color);
                
                workBenchIcon.Set(icon.Icon);
                workBenchIcon.SetAmount(0, requiredIngredientDesc.Amount);
            }
            else
            {
                workBenchIcon.gameObject.SetActive(false);
            }
        }
    }
}