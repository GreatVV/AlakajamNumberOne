using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class IconCacheComponent : IComponent
{
    public Dictionary<long, InventoryIcon> value = new Dictionary<long, InventoryIcon>();
    public Dictionary<string, InventoryIcon> Projectiles = new Dictionary<string, InventoryIcon>();
}