using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Entitas;

public class CheckHitSystem : ReactiveSystem<InputEntity>, ICleanupSystem
{
    private readonly Contexts _contexts;
    private IGroup<InputEntity> _collisions;


    public CheckHitSystem(Contexts contexts) : base(contexts.input)
    {
        _contexts = contexts;
        _collisions = _contexts.input.GetGroup(InputMatcher.Collision);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AllOf(InputMatcher.Collision));
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var inputEntity in entities)
        {
            var collision = inputEntity.collision;
            GameEntity boss;
            if (collision.Object1.hasCurrentBoss)
            {
                boss = collision.Object1;
            }
            else if (collision.Object2.hasCurrentBoss)
            {
                boss = collision.Object2;
            }
            else
            {
                boss = null;
            }
            
            if (boss != null)
            {
                var projectile = collision.Object1 == boss ? collision.Object2 : collision.Object1;
                if (projectile.hasHealth)
                {
                    var damage = projectile.health.value;
                    projectile.isDestroy = true;
                    boss.ReplaceHealth(boss.health.value - damage);
                }
            }
        }
    }

    public void Cleanup()
    {
        foreach (var inputEntity in _collisions.GetEntities())
        {
            inputEntity.Destroy();
        }
    }
}