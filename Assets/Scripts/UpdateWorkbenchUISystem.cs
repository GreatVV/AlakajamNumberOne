using System.Collections.Generic;
using System.Linq;
using Entitas;

public class UpdateWorkbenchUISystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _insideWorkbench;

    public UpdateWorkbenchUISystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _insideWorkbench = contexts.game.GetGroup(GameMatcher.InsideWorkbench);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.InsideWorkbench.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var insideWorkbench = _insideWorkbench.GetEntities();
        var recipe = _contexts.game.currentBossEntity.recipe.value;
        var workBench = _contexts.game.workbench.value;
        
        for (var index = 0; index < recipe.Ingredients.Count; index++)
        {
            var requiredIngredientDesc = recipe.Ingredients[index];
            var icon = workBench.Icons[index];
            var count = insideWorkbench.Count(x =>
                x.ingredient.value == requiredIngredientDesc.Type &&
                x.color.value == requiredIngredientDesc.Color);
            icon.SetAmount( count, requiredIngredientDesc.Amount);
        }
    }
}