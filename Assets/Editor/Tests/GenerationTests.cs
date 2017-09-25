using System.Collections;
using System.Collections.Generic;
using Entitas;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class GenerationTests 
{

	[Test]
	public void BasicGeneration()
	{
		var contexts = new Contexts();
		var game = contexts.game;

		game.SetGameField(10, 10);
		
		var systems = new GenerateItemsSystem(contexts);
		
		Assert.IsTrue(game.hasGameField);

		var generationDescription = new GenerationDescription();
		generationDescription.Ingredients = new List<GenerationItem>()
		{
			new GenerationItem()
			{
				Amount = 5,
				Type = IngredientsType.Ingot
			},
			new GenerationItem()
			{
				Amount = 3,
				Type = IngredientsType.Plant
			}
		};
		
		game.gameFieldEntity.ReplaceGenerationDescription(generationDescription);
		game.gameFieldEntity.isNeedRegenerate = true;
		
		systems.Execute();

		var ingredientsEntities = game.GetGroup(GameMatcher.Ingredient);
		
		Assert.AreEqual(8, ingredientsEntities.count);
	}

	[Test]
	public void CheckRandomPlacesTest()
	{
		var gameField = new GameFieldComponent()
		{
			Width = 20,
			Height = 10
		};

		for (int i = 0; i < 1000; i++)
		{
			var position = Helper.RandomPosition(20, 10);
			Assert.IsTrue(position.x >= -10 && position.x <= 10 && position.z >= -5 && position.z <= 5);
		}
	}

	[Test]
	public void CreateIngredientsViewTest()
	{
		var contexts = new Contexts();
		var game = contexts.game;

		game.SetGameField(10, 10);

		var factory = new GameObject().AddComponent<IngredientViewFactory>();
		factory.Inject(contexts);
		
		factory.Descs = new IngredientViewFactory.IngredientDesc[]
		{
			new IngredientViewFactory.IngredientDesc()
			{
				Type = IngredientsType.Ingot,
				View = new GameObject()
			}, 
			new IngredientViewFactory.IngredientDesc()
			{
				Type = IngredientsType.Plant,
				View = new GameObject()
			}
		};

		var systems = new Systems().Add(new GenerateItemsSystem(contexts))
			.Add(new CreateIngredientViewSystem(contexts));
		systems.Initialize();
		
		Assert.IsTrue(game.hasGameField);

		var generationDescription = new GenerationDescription();
		generationDescription.Ingredients = new List<GenerationItem>()
		{
			new GenerationItem()
			{
				Amount = 5,
				Type = IngredientsType.Ingot
			},
			new GenerationItem()
			{
				Amount = 3,
				Type = IngredientsType.Plant
			}
		};
		
		game.gameFieldEntity.ReplaceGenerationDescription(generationDescription);
		game.gameFieldEntity.isNeedRegenerate = true;
		
		systems.Execute();

		var ingredientsEntities = game.GetGroup(GameMatcher.AllOf(GameMatcher.Ingredient, GameMatcher.GameObject));
		
		Assert.AreEqual(8, ingredientsEntities.count);
	}

	[Test]
	public void ChangeBossAfterVictory()
	{
		var contexts = new Contexts();
		AddMocks(contexts);
		
		var boss1 = new Boss()
		{
			BossName = "Batman",
			Health = 10,
			GenerationDescription = new GenerationDescription()
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
			},
			PossibleRecipes = new RecipeCollection()
			{
				Recipes = new List<Recipe>()
				{
					new Recipe()
					{
						Damage = 5,
						Ingredients = new List<RequiredIngredientDesc>()
						{
							new RequiredIngredientDesc()
							{
								Amount = 1,
								Color = ColorType.Blue,
								Type = IngredientsType.Crystal
							}
						},
						Liquid = LiquidType.Water,
						Name = "Smoothie",
						ProjectilePrefab = new GameObject("Recipe 1 projectile")
					}
				}
			},
			Prefab = new GameObject("Boss 1"),
			TimeBeforeSpawn = 10,
		};

		var boss2 = new Boss()
		{
			BossName = "Superman",
			Health = 12,
			GenerationDescription = new GenerationDescription()
			{
				Ingredients = new List<GenerationItem>()
				{
					new GenerationItem()
					{
						Amount = 2,
						Color = ColorType.Red,
						Type = IngredientsType.Ingot
					}
				}
			},
			PossibleRecipes = new RecipeCollection()
			{
				Recipes = new List<Recipe>()
				{
					new Recipe()
					{
						Damage = 7,
						Ingredients = new List<RequiredIngredientDesc>()
						{
							new RequiredIngredientDesc()
							{
								Amount = 2,
								Color = ColorType.Red,
								Type = IngredientsType.Ingot
							}
						},
						Liquid = LiquidType.Water,
						Name = "Smoothie Tastie",
						ProjectilePrefab = new GameObject("Recipe 2 projectile")
					}
				}
			},
			Prefab = new GameObject("Boss 1"),
			TimeBeforeSpawn = 10,
		};
		var gameDescription = new GameObject("Game description").AddComponent<GameDescription>();
		gameDescription.Bosses = new[]
		{
			boss1,
			boss2
		};
		contexts.game.ReplaceGameDescription(gameDescription);
		
		var systems = Main.CreateSystems(contexts);
		systems.Initialize();
		systems.Execute();
		
		Assert.AreEqual(boss1, contexts.game.currentBoss.value);
		Assert.AreEqual(10, contexts.game.currentBossEntity.maxHealth.value);
		Assert.AreEqual(10, contexts.game.currentBossEntity.health.value);
		
		contexts.game.currentBossEntity.ReplaceHealth(-1);
		
		systems.Execute();
		systems.Execute();
		
		Assert.AreEqual(boss2, contexts.game.currentBoss.value);
		Assert.AreEqual(12, contexts.game.currentBossEntity.maxHealth.value);
		Assert.AreEqual(12, contexts.game.currentBossEntity.health.value);
	}

	private void AddMocks(Contexts contexts)
	{
		contexts.game.ReplaceUI(new GameObject().AddComponent<UI>());	
		contexts.game.ReplacePlayer(newPlayerDescription: new PlayerDescription()
		{
			ConstantPowerDescription = new ConstantPowerDescription(),
			GameZoneCollider = new BoxCollider(),
			Health = 10,
			Name = "Batman",
			RespawnPosition = new GameObject().transform,
			TargetGameObject = new GameObject()
		});
		contexts.game.ReplaceGameField(10, 10);
		contexts.game.ReplaceWorkbench(new GameObject().AddComponent<Workbench>());
		var ingredientViewFactory = new GameObject().AddComponent<IngredientViewFactory>();
		ingredientViewFactory.Fill();
		foreach (var ingredientDesc in ingredientViewFactory.Descs)
		{
			ingredientDesc.View = new GameObject();
		}
		contexts.game.ReplaceIngredientViewFactory(ingredientViewFactory);
		
	}
}