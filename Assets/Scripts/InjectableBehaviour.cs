using UnityEngine;

public abstract class InjectableBehaviour : MonoBehaviour
{
    protected Contexts _contexts;

    public void Inject(Contexts contexts)
    {
        _contexts = contexts;
        OnInject();
    }

    protected abstract void OnInject();
}