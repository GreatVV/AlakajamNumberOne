using NUnit.Framework;

[TestFixture]
public class BossTests
{
    [Test]
    public void HealthTest()
    {
        var contexts = new Contexts();

        var checkHitSystem = new CheckHitSystem(contexts);

        var projectileEntity = contexts.game.CreateEntity();
        //health is damage
        projectileEntity.AddHealth(4);

        //boss
        var bossEntity = contexts.game.SetCurrentBoss(new Boss() {Health = 10});
        bossEntity.AddHealth(10);
		
        var collideEntity = contexts.input.CreateEntity();
        collideEntity.AddCollision(projectileEntity, bossEntity);

        checkHitSystem.Execute();
        
        Assert.AreEqual(6, bossEntity.health.value);
    }
}