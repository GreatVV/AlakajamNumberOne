using UnityEngine;

[Game]
public class PlayerView : InjectableBehaviour
{
    public PlayerDescription Description;
    public CharacterControl CharacterControl;
    
    protected override void OnInject()
    {
        var player = _contexts.game.SetPlayer(Description);
        player.AddGameObject(Description.TargetGameObject);
        player.AddPlayerView(this);
    }
}