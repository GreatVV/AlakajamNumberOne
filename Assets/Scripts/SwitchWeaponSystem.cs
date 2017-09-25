using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SwitchWeaponSystem : ReactiveSystem<InputEntity>
{
    private Contexts _contexts;

    public SwitchWeaponSystem(Contexts contexts) : base(contexts.input)
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
        if (Input.GetMouseButtonDown(2))
        {
            _contexts.game.playerEntity.ReplaceWeapon(_contexts.game.playerEntity.weapon.WeaponType == WeaponType.ConstantPower ? WeaponType.Slingshot : WeaponType.ConstantPower);
        }
    }
}