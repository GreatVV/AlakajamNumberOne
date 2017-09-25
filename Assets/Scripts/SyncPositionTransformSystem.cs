using System.Collections.Generic;
using Entitas;

public class SyncPositionTransformSystem : ReactiveSystem<InputEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _gameObjectPosition;

    public SyncPositionTransformSystem(Contexts contexts) : base(contexts.input) 
    {
        _contexts = contexts;
        _gameObjectPosition = contexts.game.GetGroup(GameMatcher.GameObject);
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
        foreach (var entity in _gameObjectPosition.GetEntities())
        {
            entity.ReplacePosition(entity.gameObject.value.transform.position);
        }
    }
}