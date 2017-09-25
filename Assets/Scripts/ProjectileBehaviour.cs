using UnityEngine;

public class ProjectileBehaviour : InjectableBehaviour
{
    private GameObjectEntityLink _link;
    
    void Start()
    {
        _link = GetComponent<GameObjectEntityLink>();
    }
    
    protected override void OnInject()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        var link = other.GetComponent<GameObjectEntityLink>();
        if (link)
        {
            var collision = _contexts.input.CreateEntity();
            collision.AddCollision(_link.Entity, link.Entity);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        var link = collision.collider.GetComponent<GameObjectEntityLink>();
        if (link)
        {
            var collisionEntity = _contexts.input.CreateEntity();
            collisionEntity.AddCollision(_link.Entity, link.Entity);

        }
    }
}