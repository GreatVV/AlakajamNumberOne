using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class HideItemInsideWorkbenchSystem : ReactiveSystem<InputEntity>
{
    private Contexts _contexts;

    public HideItemInsideWorkbenchSystem(Contexts contexts) : base(contexts.input)
    {
        _contexts = contexts;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Collision);
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var effectManager = _contexts.game.effectManager.value;
        
        foreach (var inputEntity in entities)
        {
            
            var collision = inputEntity.collision;
            //Debug.LogFormat("Collide {0} with {1}", collision.Object1, collision.Object2);
            var workBenchEntity = collision.Object1.hasWorkbench
                ? collision.Object1
                : collision.Object2.hasWorkbench
                    ? collision.Object2
                    : null;
            if (workBenchEntity != null)
            {
                var projectile = collision.Object1 == workBenchEntity ? collision.Object2 : collision.Object1;
                if (projectile.hasIngredient && projectile.usage.value == UsageType.Craft)
                {
                    var recipe = _contexts.game.currentBossEntity.recipe.value;
                    if (recipe.Ingredients.Any(x =>
                        x.Color == projectile.color.value && x.Type == projectile.ingredient.value))
                    {

                        projectile.gameObject.value.SetActive(false);
                        projectile.isInsideWorkbench = true;

                        var particle = Object.Instantiate(effectManager.HitWorkbenchPrefab,
                            projectile.gameObject.value.transform.position, Quaternion.identity);
                        Object.Destroy(particle.gameObject, 0.5f);
                    }
                    else
                    {
                        var particle = Object.Instantiate(effectManager.IncorrectHitWorkbenchPrefab,
                            projectile.gameObject.value.transform.position, Quaternion.identity);
                        Object.Destroy(particle.gameObject, 0.5f);
                    }
                    
                }
            }
        }
    }
}