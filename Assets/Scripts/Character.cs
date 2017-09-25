using UnityEngine;

public class Character : InjectableBehaviour
{
    public Camera Camera;
    public float Distance = 3;
    
    protected override void OnInject()
    {
        _contexts.game.SetCamera(Camera).AddDistance(Distance);
    }
}