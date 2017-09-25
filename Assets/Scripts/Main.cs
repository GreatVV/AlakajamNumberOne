using Entitas;
using UnityEngine;

public class Main : MonoBehaviour 
{
	private Systems _systems;
	
	public InjectableBehaviour[] Injectables;

	[ContextMenu("Refresh")]
	public void Grab()
	{
		Injectables = FindObjectsOfType<InjectableBehaviour>();
	}
	
	public void Start()
	{
		var contexts = new Contexts();
		foreach (var injectable in Injectables)
		{
			injectable.Inject(contexts);
		}
		_systems = CreateSystems(contexts);

		_systems.Initialize();
	}

	void Update()
	{
		_systems.Execute();
		_systems.Cleanup();
	}

	void OnDestroy()
	{
		_systems.TearDown();
	}

	public static Systems CreateSystems(Contexts contexts)
	{
		return new Feature("Game")
			.Add(new UpdateTickSystem(contexts))
			.Add(new ControlCharacterSystem(contexts))
			.Add(new PauseGameSystem(contexts))
			.Add(new SwitchWeaponSystem(contexts))
			.Add(new SyncPositionTransformSystem(contexts))
				
			.Add(new StartGameSystem(contexts))
			.Add(new GenerateItemsSystem(contexts))
			.Add(new CreateIngredientViewSystem(contexts))
			//
			.Add(new HideInInventorySystem(contexts))
			.Add(new BossChangeSystem(contexts))
			.Add(new UpdateWorkbenchRecipeSystem(contexts))
			//
			.Add(new UpdateTimerSystem(contexts))
			.Add(new SpawnBossSystem(contexts))
			.Add(new ShootingSystem(contexts))
				//
			
			.Add(new UpdateHealthBarSystem(contexts))
			.Add(new RespawnSystem(contexts))
				//
			.Add(new UpdateInventoryUISystem(contexts))
			.Add(new UpdateChoseItemUISystem(contexts))
			.Add(new ChangeWeaponSystem(contexts))
			.Add(new UpdateCurrentProjectileSystem(contexts))
			.Add(new KillBossSystem(contexts))
				//craft
				.Add(new HideItemInsideWorkbenchSystem(contexts))
				.Add(new CraftingSystem(contexts))
				.Add(new UpdateWorkbenchUISystem(contexts))
			
			//
			.Add(new InputSystem(contexts))
			.Add(new SlingshotShotPowerSystem(contexts))
			.Add(new RaycastSystem(contexts))
			.Add(new CheckHitSystem(contexts))
			.Add(new SpawnEffectOnHitSystem(contexts))
			.Add(new UpdateWeaponStateSystem(contexts))
			.Add(new DestroySystem(contexts))
			;
	}
}