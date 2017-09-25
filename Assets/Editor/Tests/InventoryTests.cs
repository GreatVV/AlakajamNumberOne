using System.Collections;
using System.Collections.Generic;
using Entitas;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[TestFixture]
public class InventoryTests
{
    [Test]
    public void InventoryGrabTest()
    {
        var contexts = new Contexts();


        //create item
        var cubeEntity = contexts.game.CreateEntity();
        cubeEntity.AddIngredient(IngredientsType.Crystal);

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var link = cube.AddComponent<GameObjectEntityLink>();
        link.Entity = cubeEntity;
        cubeEntity.AddGameObject(cube);

        cube.transform.position = new Vector3(5, 0, 0);

        var camera = new GameObject().AddComponent<Camera>();
        camera.transform.LookAt(cube.transform, Vector3.up);
        contexts.game.SetCamera(camera);
        contexts.game.cameraEntity.AddDistance(float.MaxValue);

        var tryGrabSystem = new RaycastSystem(contexts);

        var click = contexts.input.CreateEntity();
        click.isClick = true;

        Assert.IsFalse(cubeEntity.isInsideInventory);

        tryGrabSystem.Execute();

        Assert.IsTrue(cubeEntity.isInsideInventory);
    }

    [Test]
    public void ValidateGenerationDescriptionFail()
    {
        var gameDescription = new Boss()
        {
        };
        var recipe = new Recipe()
        {
            Ingredients = new List<RequiredIngredientDesc>()
            {
                new RequiredIngredientDesc()
                {
                    Amount = 1,
                    Color = ColorType.Blue,
                    Type = IngredientsType.Crystal
                }
            }
        };

        var possibleItems = new GenerationDescription();

        Assert.IsFalse(Helper.Validate(recipe, possibleItems));
    }

    [Test]
    public void ValidateGenerationDescriptionSuccess()
    {
        var gameDescription = new Boss()
        {
        };
        var recipe = new Recipe()
        {
            Ingredients = new List<RequiredIngredientDesc>()
            {
                new RequiredIngredientDesc()
                {
                    Amount = 1,
                    Color = ColorType.Blue,
                    Type = IngredientsType.Crystal
                }
            }
        };

        var possibleItems = new GenerationDescription()
        {
            Ingredients = new List<GenerationItem>()
            {
                new GenerationItem()
                {
                    Amount = 1,
                    Color = ColorType.Blue,
                    Type = IngredientsType.Crystal
                }
            }
        };

        Assert.IsTrue(Helper.Validate(recipe, possibleItems));
    }

    [Test]
    public void ShowInInventoryTest()
    {
        var contexts = new Contexts();

        UI ui = new GameObject().AddComponent<UI>();
        ui.CraftingIngredients = new InventoryLayout();
        ui.CraftingIngredients.Prefab = new GameObject().AddComponent<InventoryIcon>();
        contexts.game.SetUI(ui);
        
        var system = new UpdateInventoryUISystem(contexts);

        //create item
        var cubeEntity = contexts.game.CreateEntity();
        cubeEntity.AddIngredient(IngredientsType.Crystal);

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var link = cube.AddComponent<GameObjectEntityLink>();
        link.Entity = cubeEntity;
        cubeEntity.AddGameObject(cube);
        cubeEntity.isInsideInventory = true;
        cubeEntity.AddUsage(UsageType.Craft);
        
        system.Execute();
        
        Assert.IsTrue(cubeEntity.hasInventoryIcon);
    }
}

