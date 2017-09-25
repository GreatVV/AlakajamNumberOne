using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class UpdateInventoryUISystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private Contexts _contexts;
    private IGroup<GameEntity> _icons;

    public UpdateInventoryUISystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _icons = contexts.game.GetGroup(GameMatcher.InventoryIcon);
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
        var ui = _contexts.game.uI.value;
        var iconsCache = _contexts.game.iconCache;
        foreach (var entity in entities)
        {
            if (entity.isInsideInventory && !entity.hasInventoryIcon)
            {
                if (!entity.hasIcon)
                {
                    Debug.LogWarning("No icon for "+entity);
                }
                
                InventoryLayout inventoryLayout = null;
                switch (entity.usage.value)
                {
                    case UsageType.Craft:
                        inventoryLayout = ui.CraftingIngredients;
                        break;
                    case UsageType.Weapon:
                        inventoryLayout = ui.Projectiles;
                        break;
                    default:
                        continue;
                }

                var prefab = inventoryLayout.Prefab;
                InventoryIcon instance;

                switch (entity.usage.value)
                {
                    case UsageType.Craft:
                    {
                        var id = Helper.GetId(entity.ingredient.value, entity.color.value);
                        if (!iconsCache.value.TryGetValue(id, out instance))
                        {
                            instance = Object.Instantiate(prefab, inventoryLayout.Root, false);
                            iconsCache.value[id] = instance;
                        }
                    }
                        break;
                    default:
                    {
                        var projectileIconCache = iconsCache.Projectiles;
                        var id = entity.recipe.value.Name;
                        if (!projectileIconCache.TryGetValue(id, out instance))
                        {
                            instance = Object.Instantiate(prefab, inventoryLayout.Root, false);
                            projectileIconCache[id] = instance;
                        }
                    }
                        break;
                }
                if (entity.hasIcon)
                {
                    instance.Set(entity.icon.value);
                }
                entity.AddInventoryIcon(instance);
            }

            if (entity.isInsideInventory)
            {
                entity.inventoryIcon.value.AddAmount();
            }
            else
            {
                entity.inventoryIcon.value.DecreaseAmount();
            }
        }
        
    }


    public void Initialize()
    {
        _contexts.game.SetIconCache(new Dictionary<long, InventoryIcon>(), new Dictionary<string, InventoryIcon>());
    }
}