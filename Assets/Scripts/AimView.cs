using UnityEngine;

[Game]
public class AimView : InjectableBehaviour
{
    public Vector3 MinSize = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 MaxSize = new Vector3(1f, 1f, 1f);
    public Transform TargetTransform;
    
    protected override void OnInject()
    {
        _contexts.game.slingshotDescriptionEntity.AddAimView(this);
    }
}