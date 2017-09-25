using Entitas;

public class UpdateTickSystem : IExecuteSystem, IInitializeSystem
{
    private Contexts _contexts;

    public UpdateTickSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        if (!_contexts.input.tickEntity.isPause)
        {
            _contexts.input.ReplaceTick(_contexts.input.tick.Tick + 1);
        }
    }

    public void Initialize()
    {
        _contexts.input.ReplaceTick(0);
    }
}