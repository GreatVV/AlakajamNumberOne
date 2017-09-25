using System;
using System.Collections.Generic;
using Entitas;

public class UpdateTimerSystem : ReactiveSystem<InputEntity>
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _timers;

    public UpdateTimerSystem(Contexts contexts)	: base(contexts.input)
    {
        _contexts = contexts;
        _timers = _contexts.game.GetGroup(GameMatcher.TimerBehaviour);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Tick);
    }

    protected override bool Filter(InputEntity entity)
    {
        return _contexts.game.hasCurrentBoss && _contexts.game.currentBossEntity.hasTime;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var bossEntity = _contexts.game.currentBossEntity;
        var boss = bossEntity.currentBoss.value;
        var startTime = bossEntity.startTime.value;
        var timeTo = boss.TimeBeforeSpawn;
        bossEntity.ReplaceTime(timeTo - (float) (DateTime.Now - startTime).TotalSeconds  );

        if (bossEntity.time.value > 0)
        {
            foreach (var timerBehaviour in _timers.GetEntities())
            {
                timerBehaviour.timerBehaviour.value.SetSeconds(bossEntity.time.value);
            }
        }
        else
        {
            foreach (var timerBehaviour in _timers.GetEntities())
            {
                timerBehaviour.timerBehaviour.value.Hide();
            }
        }
    }
}