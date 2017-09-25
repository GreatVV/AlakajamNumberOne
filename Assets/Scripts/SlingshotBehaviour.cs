using UnityEngine;

public class SlingshotBehaviour : InjectableBehaviour
{
    [SerializeField] private SlingshotDescription _slingshotDescription;

    protected override void OnInject()
    {
        _contexts.game.SetSlingshotDescription(_slingshotDescription);
    }
}