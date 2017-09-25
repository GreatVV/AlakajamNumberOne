using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class UpdateCurrentProjectileSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _inventory;

    public UpdateCurrentProjectileSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _inventory = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.InsideInventory, GameMatcher.Usage));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.InsideInventory.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var currentWeapon = _contexts.game.playerEntity.weapon.WeaponType;
        UsageType usage = UsageType.None;
        switch (currentWeapon)
        {
            case WeaponType.ConstantPower:
                usage = UsageType.Craft;
                break;
            case WeaponType.Slingshot:
                usage = UsageType.Weapon;
                break;
        }

        if (!_contexts.game.playerEntity.hasCurrentProjectile || _contexts.game.playerEntity.currentProjectile.value == null || !_contexts.game.playerEntity.currentProjectile.value.isInsideInventory)
        {
            var availableItems = _inventory.GetEntities().Where(x => x.usage.value == usage);
            if (availableItems.Any())
            {
                _contexts.game.playerEntity.ReplaceCurrentProjectile(availableItems.First());
            }
        }
    }
}