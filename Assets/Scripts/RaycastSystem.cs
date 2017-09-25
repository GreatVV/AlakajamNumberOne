using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Entitas;
using UnityEngine;

public class RaycastSystem : ReactiveSystem<InputEntity>, ICleanupSystem
{
    private Contexts _contexts;

    private IGroup<InputEntity> _cleanUptarget;
    
    public RaycastSystem(Contexts contexts) : base(contexts.input)
    {
        _contexts = contexts;
        _cleanUptarget = contexts.input.GetGroup(InputMatcher.Click);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Click);
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var click = entities.First();
        var camera = _contexts.game.camera.value;
        var maxDistance = _contexts.game.cameraEntity.distance.value;
        var ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            var link = hitInfo.collider.gameObject.GetComponent<GameObjectEntityLink>();
            if (link)
            {
                if (link.Entity.hasIngredient || link.Entity.hasUsage)
                {
                    if (Vector3.Distance(link.transform.position,
                            _contexts.game.playerEntity.gameObject.value.transform.position) < maxDistance)
                    {
                        link.Entity.isInsideInventory = true;
                        link.Entity.isProjectile = false;
                        if (link.Entity.hasHealth)
                        {
                            link.Entity.RemoveHealth();
                        }

                        if (link.Entity.hasOwner)
                        {
                            link.Entity.RemoveOwner();
                        }
                    }
                }
            }

        }

    }

    public void Cleanup()
    {
        foreach(var input in _cleanUptarget.GetEntities())
        {
            input.Destroy();
        }
    }
}