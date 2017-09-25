using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CreateIngredientViewSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public CreateIngredientViewSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Ingredient).NoneOf(GameMatcher.GameObject));
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.hasGameObject;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var factory = _contexts.game.ingredientViewFactory.value;
        var gameField = _contexts.game.gameField;
        foreach (var entity in entities)
        {
            var desc = factory.ById(entity.ingredient.value, entity.hasColor ? entity.color.value : ColorType.None);
            var prefab = desc.View;
            var position = Helper.RandomPosition(gameField.Width, gameField.Height);
            var instance = Object.Instantiate(prefab, position, Quaternion.identity);
            entity.AddGameObject(instance);
            entity.AddIcon(desc.Icon);
            var link = instance.gameObject.AddComponent<GameObjectEntityLink>();
            link.Entity = entity;
        }
    }
}