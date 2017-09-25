using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class UpdateWeaponStateSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public UpdateWeaponStateSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Weapon);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var isSlingshot = entities.First().weapon.WeaponType == WeaponType.Slingshot;
        var ui = _contexts.game.uI.value;
        ui.SlingshotIcon.gameObject.SetActive(isSlingshot);
    }
}