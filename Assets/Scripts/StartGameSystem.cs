using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

public class StartGameSystem : IInitializeSystem
{
    private Contexts _contexts;

    public StartGameSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var gameDesc = _contexts.game.gameDescription.value;
        var field = _contexts.game.gameFieldEntity;
        var ui = _contexts.game.uI.value;
        var boss = gameDesc.Bosses[0];
        _contexts.game.SetCurrentBoss(boss);
        if (ui.BossHealthBar != null)
        {
            _contexts.game.currentBossEntity.AddHealthBarBehaviour(ui.BossHealthBar);
        } else
        {
            Debug.LogWarning("No boss health bar");
        };

        var player = _contexts.game.playerEntity;
        
        if (ui.PlayerHealthBar)
        {
            player.AddHealthBarBehaviour(ui.PlayerHealthBar);
        }
        else
        {
            Debug.LogWarning("No player health bar");
        }
        
        player.AddHealth(player.player.PlayerDescription.Health);
        player.AddMaxHealth(player.player.PlayerDescription.Health);
        player.AddWeapon(WeaponType.ConstantPower);
    }
}