using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class SlingshotTest
{
    [Test]
    public void PowerTest()
    {
        var maxPower = 10f;
        var minPower = 1f;

        var timePassed = 1f;
        var maxPowerTime = 2f;

        var newPower = Helper.GetPower(minPower, maxPower, timePassed, maxPowerTime);
        Assert.AreEqual(5.5f, newPower);
    }

    [Test]
    public void PowerByPressSystem()
    {
        var contexts = new Contexts();
        var power = new SlingshotShotPowerSystem(contexts);
        
        var slingshotDesc = new SlingshotDescription();
        slingshotDesc.MaxPower = 10;
        slingshotDesc.MinPower = 0;
        slingshotDesc.MaxPowerTime = 2;
        contexts.game.SetSlingshotDescription(slingshotDesc);
        
        //simulate right press
        contexts.input.isRightMousePress = true;
        contexts.input.rightMousePressEntity.AddTime(10); //time is Time.realtimeSinceStartup 
        
        //left mouse click
        var input = contexts.input.CreateEntity();
        input.isClick = true;
        input.AddTime(11);
        
        Assert.IsFalse(contexts.game.slingshotDescriptionEntity.hasShotPower);
        
        power.Execute();
        
        Assert.IsTrue(contexts.game.slingshotDescriptionEntity.hasShotPower );
        Assert.AreEqual(5, contexts.game.slingshotDescriptionEntity.shotPower.value);
        
    }

    [Test]
    public void ChangeWeaponType()
    {
        var contexts = new Contexts();

        var changeWeaponSystem = new ChangeWeaponSystem(contexts);
        
        PlayerDescription playerDescription = new PlayerDescription()
        {
            ConstantPowerDescription = new ConstantPowerDescription()
            {
                ShowPower = 10
            }
        };
        
        var player = contexts.game.SetPlayer(playerDescription);
        player.ReplaceWeapon(WeaponType.ConstantPower);

        changeWeaponSystem.Execute();
        
        Assert.IsTrue(player.hasWeapon);
        Assert.IsTrue(player.hasShotPower);
        Assert.IsFalse(player.hasCurrentProjectile);

    }

    [Test]
    public void ChangeWeaponAndSetDefaultShooting()
    {
        var contexts = new Contexts();

        var changeWeaponSystem = new ChangeWeaponSystem(contexts);

        var craftingElement = contexts.game.CreateEntity();
        craftingElement.AddUsage(UsageType.Craft);
        craftingElement.isInsideInventory = true;
        craftingElement.AddIngredient(IngredientsType.Crystal);
        
        PlayerDescription playerDescription = new PlayerDescription()
        {
            ConstantPowerDescription = new ConstantPowerDescription()
            {
                ShowPower = 10
            }
        };
        
        var player = contexts.game.SetPlayer(playerDescription);
        player.ReplaceWeapon(WeaponType.ConstantPower);

        changeWeaponSystem.Execute();
        
        Assert.IsTrue(player.hasWeapon);
        Assert.IsTrue(player.hasShotPower);
        Assert.IsTrue(player.hasCurrentProjectile);
        Assert.AreEqual(craftingElement, player.currentProjectile.value);
    }
}