using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine.Experimental.Rendering;
using Object = UnityEngine.Object;

public class CraftingSystem : ReactiveSystem<GameEntity>
{   
    private Contexts _contexts;

    private IGroup<GameEntity> _insideWorkbench;
    
    public CraftingSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _insideWorkbench = contexts.game.GetGroup(GameMatcher.InsideWorkbench);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.InsideWorkbench);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }
    
    private Dictionary<long, int> _recipeItems = new Dictionary<long, int>();
    private List<GameEntity> _used = new List<GameEntity>();

    protected override void Execute(List<GameEntity> entities)
    {
        var insideWorkbench = _insideWorkbench.GetEntities();
        var recipe = _contexts.game.currentBossEntity.recipe.value;
        _recipeItems.Clear();
        _used.Clear();

        var correct = true;
        foreach (var requiredIngredientDesc in recipe.Ingredients)
        {
            _recipeItems[Helper.GetId(requiredIngredientDesc.Type, requiredIngredientDesc.Color)] =
                requiredIngredientDesc.Amount;
            
        }

        foreach (var insideItem in insideWorkbench)
        {
            var id = Helper.GetId(insideItem.ingredient.value, insideItem.color.value);
            var amount = 0;
            if (_recipeItems.TryGetValue(id, out amount))
            {
                if (amount > 0)
                {
                    _recipeItems[id] = amount - 1;
                    _used.Add(insideItem);
                }
            }
            else
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            if (_recipeItems.All(x => x.Value == 0))
            {

                var newProjectile = _contexts.game.CreateEntity();
                var projectileInstance = Object.Instantiate(recipe.ProjectilePrefab);
                projectileInstance.SetActive(false);
                newProjectile.AddGameObject(projectileInstance);
                newProjectile.isInsideInventory = true;
                newProjectile.AddUsage(UsageType.Weapon);
                newProjectile.AddRecipe(recipe);
                newProjectile.AddIcon(recipe.Icon);

                var link = projectileInstance.AddComponent<GameObjectEntityLink>();
                link.Entity = newProjectile;

                foreach (var gameEntity in _used)
                {
                    gameEntity.isDestroy = true;
                }
            }
        }
    }
}