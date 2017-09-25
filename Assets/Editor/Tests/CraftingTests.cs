using System.Collections.Generic;
using System.Linq;
using Entitas;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class CraftingTests
{
    [Test]
    public void CraftCorrectCase()
    {
        var contexts = new Contexts();
        var createWorkbench = new Systems()
            .Add(new CraftingSystem(contexts))
            .Add(new DestroySystem(contexts));
            ;

        var boss = new Boss();
        var bossEntity = contexts.game.SetCurrentBoss(boss);
        bossEntity.AddRecipe(new Recipe()
        {
            Ingredients = new List<RequiredIngredientDesc>()
            {
                new RequiredIngredientDesc()
                {
                    Amount = 1,
                    Color = ColorType.Blue,
                    Type = IngredientsType.Crystal
                }
            },
            ProjectilePrefab = new GameObject(),
            Damage = 10,
            Liquid = LiquidType.Alcohol,
            Name = "Smoothie"
        });

        var projectileItemsGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Usage));
        Assert.AreEqual(0, projectileItemsGroup.count);
        
        var ingredient = contexts.game.CreateEntity();
        ingredient.AddIngredient(IngredientsType.Crystal);
        ingredient.AddColor(ColorType.Blue);
        ingredient.AddUsage(UsageType.Craft);
        ingredient.isInsideWorkbench = true;
        
        createWorkbench.Execute();
        
        Assert.AreEqual(0, projectileItemsGroup.GetEntities().Where(x=>x.usage.value == UsageType.Craft).Count());
        Assert.AreEqual(1, projectileItemsGroup.GetEntities().Where(x=>x.usage.value == UsageType.Weapon).Count());

    }
    
    [Test]
    public void CraftInCorrectCase()
    {
        var contexts = new Contexts();
        var createWorkbench = new Systems()
            .Add(new CraftingSystem(contexts))
            .Add(new DestroySystem(contexts));
        ;

        var boss = new Boss();
        var bossEntity = contexts.game.SetCurrentBoss(boss);
        bossEntity.AddRecipe(new Recipe()
        {
            Ingredients = new List<RequiredIngredientDesc>()
            {
                new RequiredIngredientDesc()
                {
                    Amount = 1,
                    Color = ColorType.Blue,
                    Type = IngredientsType.Crystal
                }
            },
            ProjectilePrefab = new GameObject(),
            Damage = 10,
            Liquid = LiquidType.Alcohol,
            Name = "Smoothie"
        });

        var projectileItemsGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Usage));
        Assert.AreEqual(0, projectileItemsGroup.count);
        
        var ingredient = contexts.game.CreateEntity();
        ingredient.AddIngredient(IngredientsType.Crystal);
        ingredient.AddColor(ColorType.Green);
        ingredient.AddUsage(UsageType.Craft);
        ingredient.isInsideWorkbench = true;
        
        createWorkbench.Execute();
        
        Assert.AreEqual(1, projectileItemsGroup.GetEntities().Where(x=>x.usage.value == UsageType.Craft).Count());
        Assert.AreEqual(0, projectileItemsGroup.GetEntities().Where(x=>x.usage.value == UsageType.Weapon).Count());

    }

    [Test]
    public void TwoItemCorrectRecipe()
    {
        var contexts = new Contexts();
        var createWorkbench = new Systems()
            .Add(new CraftingSystem(contexts))
            .Add(new DestroySystem(contexts));
        ;

        var boss = new Boss();
        var bossEntity = contexts.game.SetCurrentBoss(boss);
        bossEntity.AddRecipe(new Recipe()
        {
            Ingredients = new List<RequiredIngredientDesc>()
            {
                new RequiredIngredientDesc()
                {
                    Amount = 1,
                    Color = ColorType.Blue,
                    Type = IngredientsType.Crystal
                },
                new RequiredIngredientDesc()
                {
                    Amount = 1, Color = ColorType.Green, Type = IngredientsType.Ingot
                }
            },
            ProjectilePrefab = new GameObject(),
            Damage = 10,
            Liquid = LiquidType.Alcohol,
            Name = "Smoothie"
        });

        var projectileItemsGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Usage));
        Assert.AreEqual(0, projectileItemsGroup.count);

        {
            var ingredient = contexts.game.CreateEntity();
            ingredient.AddIngredient(IngredientsType.Crystal);
            ingredient.AddColor(ColorType.Blue);
            ingredient.AddUsage(UsageType.Craft);
            ingredient.isInsideWorkbench = true;
        }
        {
            var ingredient = contexts.game.CreateEntity();
            ingredient.AddIngredient(IngredientsType.Ingot);
            ingredient.AddColor(ColorType.Green);
            ingredient.AddUsage(UsageType.Craft);
            ingredient.isInsideWorkbench = true;
        }

        createWorkbench.Execute();
        
        Assert.AreEqual(0, projectileItemsGroup.GetEntities().Where(x=>x.usage.value == UsageType.Craft).Count());
        Assert.AreEqual(1, projectileItemsGroup.GetEntities().Where(x=>x.usage.value == UsageType.Weapon).Count());
    }

    [Test]
    public void TooManyItemsInside()
    {
        var contexts = new Contexts();
        var createWorkbench = new Systems()
            .Add(new CraftingSystem(contexts))
            .Add(new DestroySystem(contexts));
        ;

        var boss = new Boss();
        var bossEntity = contexts.game.SetCurrentBoss(boss);
        bossEntity.AddRecipe(new Recipe()
        {
            Ingredients = new List<RequiredIngredientDesc>()
            {
                new RequiredIngredientDesc()
                {
                    Amount = 1,
                    Color = ColorType.Blue,
                    Type = IngredientsType.Crystal
                }
            },
            ProjectilePrefab = new GameObject(),
            Damage = 10,
            Liquid = LiquidType.Alcohol,
            Name = "Smoothie"
        });

        var projectileItemsGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Usage));
        Assert.AreEqual(0, projectileItemsGroup.count);
        {
            var ingredient = contexts.game.CreateEntity();
            ingredient.AddIngredient(IngredientsType.Crystal);
            ingredient.AddColor(ColorType.Green);
            ingredient.AddUsage(UsageType.Craft);
            ingredient.isInsideWorkbench = true;
        }
        {
            var ingredient = contexts.game.CreateEntity();
            ingredient.AddIngredient(IngredientsType.Crystal);
            ingredient.AddColor(ColorType.Green);
            ingredient.AddUsage(UsageType.Craft);
            ingredient.isInsideWorkbench = true;
        }

        createWorkbench.Execute();
        
        Assert.AreEqual(2, projectileItemsGroup.GetEntities().Where(x=>x.usage.value == UsageType.Craft).Count());
        Assert.AreEqual(0, projectileItemsGroup.GetEntities().Where(x=>x.usage.value == UsageType.Weapon).Count());
    }
}