using UnityEngine;

public class GameObjectEntityLink : MonoBehaviour
{
    public bool DestroyEntityOnDestroy = true;
    
    public GameEntity Entity;

    void OnDestroy()
    {
        if (Entity != null && Entity.isEnabled && DestroyEntityOnDestroy)
        {
            Entity.Destroy();
        }
    }
}