using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class GameDescription : InjectableBehaviour
{
    public Boss[] Bosses;

    protected override void OnInject()
    {
        _contexts.game.SetGameDescription(this);
    }
}