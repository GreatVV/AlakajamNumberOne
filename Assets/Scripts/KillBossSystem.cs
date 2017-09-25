using System;
using System.Collections.Generic;
using Entitas;
using System.Linq;
using UnityEngine;

public class KillBossSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public KillBossSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CurrentBoss, GameMatcher.Health));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasGameObject;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var boss = entities.SingleEntity();
        if (boss.health.value <= 0)
        {
            var gameDescriptionValue = _contexts.game.gameDescription.value;
            var bossIndex = Array.IndexOf(gameDescriptionValue.Bosses, boss.currentBoss.value);
            if (bossIndex == -1)
            {
                bossIndex = 0;
            }
            
            if (bossIndex == gameDescriptionValue.Bosses.Length - 1)
            {
                Debug.Log("Final and epic win");
                _contexts.game.endGameBehaviour.value.FinalCutScene.Play();
            }
            else
            {
                bossIndex++;
                var newBoss = gameDescriptionValue.Bosses[bossIndex];
                var gameObject = _contexts.game.currentBossEntity.gameObject.value;
                UnityEngine.Object.Destroy(gameObject, 1);
                _contexts.game.currentBossEntity.RemoveGameObject();
                _contexts.game.ReplaceCurrentBoss(newBoss);
            }
        }
    }
}