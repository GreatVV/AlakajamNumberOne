using Entitas.CodeGeneration.Attributes;
using UnityEngine.Playables;

[Game, Unique]
public class EndGameBehaviour : InjectableBehaviour
{
    public PlayableDirector FinalCutScene;

    public PlayableDirector LoseCutScene;
    
    protected override void OnInject()
    {
        _contexts.game.SetEndGameBehaviour(this);
    }
}