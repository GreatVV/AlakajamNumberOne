using Entitas.CodeGeneration.Attributes;
using TMPro;
using UnityEngine;

[Game, Unique]
public class Workbench : InjectableBehaviour
{
    public Collider TargetCollider;

    public InventoryIcon[] Icons;
    
    protected override void OnInject()
    {
        var workbench = _contexts.game.SetWorkbench(this);
        workbench.AddGameObject(TargetCollider.gameObject);
        var link = TargetCollider.gameObject.AddComponent<GameObjectEntityLink>();
        link.Entity = workbench;
    }
}