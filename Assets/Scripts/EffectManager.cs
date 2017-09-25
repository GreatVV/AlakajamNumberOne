using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class EffectManager : InjectableBehaviour
{
    public GameObject HitEffectPrefab;
    public GameObject HitWorkbenchPrefab;
    [SerializeField] public GameObject IncorrectHitWorkbenchPrefab;

    protected override void OnInject()
    {
        _contexts.game.SetEffectManager(this);
    }
}