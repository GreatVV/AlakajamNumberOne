using System;
using System.Collections.Generic;
using System.Diagnostics;
using Entitas;

public class BossChangeSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _workbenchItems;
    
    public BossChangeSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _workbenchItems = _contexts.game.GetGroup(GameMatcher.InsideWorkbench);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CurrentBoss);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCurrentBoss;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var bossEntity = entities.SingleEntity();
        var boss = bossEntity.currentBoss.value;
        
        var gameDesc = _contexts.game.gameDescription.value;
        var field = _contexts.game.gameFieldEntity;
        
        field.ReplaceGenerationDescription(boss.GenerationDescription);
        field.isNeedRegenerate = true;

        var recipe = boss.PossibleRecipes.Recipes[UnityEngine.Random.Range(0, boss.PossibleRecipes.Recipes.Count)];
        bossEntity.ReplaceRecipe(recipe);
        bossEntity.ReplaceHealth(boss.Health);
        bossEntity.ReplaceMaxHealth(boss.Health);
        bossEntity.ReplaceTime(boss.TimeBeforeSpawn);
        bossEntity.ReplaceStartTime(DateTime.Now);

        foreach (var item in _workbenchItems.GetEntities())
        {
            item.isInsideWorkbench = false;
            item.isInsideInventory = true;
        }
    }
}