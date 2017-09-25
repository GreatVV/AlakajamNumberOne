using System;
using Entitas.CodeGeneration.Attributes;
using UnityEngine.UI;

[Game, Unique]
public class UI : InjectableBehaviour
{
    public HealthBarBehaviour PlayerHealthBar;
    public HealthBarBehaviour BossHealthBar;

    public InventoryLayout CraftingIngredients;
    public InventoryLayout Projectiles;
    public Image SlingshotIcon;

    protected override void OnInject()
    {
        _contexts.game.SetUI(this);
    }
}