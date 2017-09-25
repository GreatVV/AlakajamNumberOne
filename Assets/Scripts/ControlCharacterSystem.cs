using System.Collections.Generic;
using Entitas;

public class ControlCharacterSystem : ReactiveSystem<InputEntity>
{
    private Contexts _contexts;

    public ControlCharacterSystem(Contexts contexts) : base(contexts.input)
    {
        _contexts = contexts;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Tick);
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        _contexts.game.playerEntity.playerView.value.CharacterControl.UpdateTick();
    }
}