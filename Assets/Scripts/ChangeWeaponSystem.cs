using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Entitas;

public class ChangeWeaponSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _inventory;

    public ChangeWeaponSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _inventory = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.InsideInventory, GameMatcher.Usage));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Weapon, GameMatcher.Player));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var player = entities.SingleEntity();
        PlayerDescription playerDesc = player.player.PlayerDescription;
        switch (player.weapon.WeaponType)
        {
            case WeaponType.ConstantPower:
            {
                player.ReplaceShotPower(playerDesc.ConstantPowerDescription.ShowPower);
                var inventoryItem = _inventory.GetEntities().FirstOrDefault(x => x.usage.value == UsageType.Craft);
                if (inventoryItem != null)
                {
                    player.ReplaceCurrentProjectile(inventoryItem);
                }
                else
                {
                    if (player.hasCurrentProjectile)
                    {
                        player.RemoveCurrentProjectile();
                    }
                }
            }
                break;
            case WeaponType.Slingshot:
            {
                if (player.hasShotPower)
                {
                    player.RemoveShotPower();
                }

                var inventoryItem = _inventory.GetEntities().FirstOrDefault(x => x.usage.value == UsageType.Weapon);
                if (inventoryItem != null)
                {
                    player.ReplaceCurrentProjectile(inventoryItem);
                }
                else
                {
                    if (player.hasCurrentProjectile)
                    {
                        player.RemoveCurrentProjectile();
                    }
                }
            }
                break;
        }
    }
}