using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ShootingSystem : ReactiveSystem<InputEntity>
{
    private Contexts _contexts;

    public ShootingSystem(Contexts contexts) : base(contexts.input)
    {
        _contexts = contexts;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Click);
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        if (!_contexts.input.isRightMousePress)
        {
            return;
        }

        if (!_contexts.game.playerEntity.hasWeapon || !_contexts.game.playerEntity.hasCurrentProjectile)
        {
            return;
        }

        var weapon = _contexts.game.playerEntity.weapon.WeaponType;

        var projectile = _contexts.game.playerEntity.currentProjectile.value;

        //shoot if left pressed and right button
        var camera = _contexts.game.camera.value;
        var recipeValue = _contexts.game.currentBossEntity.recipe.value;

        if (projectile.usage.value == UsageType.Weapon)
        {
            projectile.AddHealth(recipeValue.Damage);
        }
        projectile.ReplaceOwner(Owner.Player);
        projectile.isProjectile = true;
        projectile.isInsideInventory = false;
        _contexts.game.playerEntity.RemoveCurrentProjectile();

        var instance = projectile.gameObject.value;
        instance.SetActive(true);
        var projectileBehaviour =
            instance.GetComponent<ProjectileBehaviour>() ?? instance.AddComponent<ProjectileBehaviour>();
        projectileBehaviour.Inject(_contexts);
        var rigidbody = instance.GetComponent<Rigidbody>();

        var shotPower = _contexts.game.playerEntity.shotPower.value;
        
        instance.transform.position = camera.transform.position;

        var totalShotVector = camera.transform.forward * shotPower;
        var totalShotPower = camera.transform.forward * shotPower;

        rigidbody.AddForce(totalShotVector, ForceMode.Impulse);
        _contexts.input.isRightMousePress = false;
        Debug.LogFormat("Spawn at {0} with power {1}", instance.transform.position,
            shotPower);
    }
}