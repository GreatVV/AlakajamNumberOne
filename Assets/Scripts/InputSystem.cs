using Entitas;
using UnityEngine;

public class InputSystem : IExecuteSystem
{
    private Contexts _contexts;

    public InputSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var click = _contexts.input.CreateEntity();
            click.isClick = true;
            click.ReplaceTime(Time.realtimeSinceStartup);
        }

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(1))
            {
                _contexts.input.isRightMousePress = true;
                _contexts.input.rightMousePressEntity.ReplaceTime(Time.realtimeSinceStartup);
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(1))
            {
                _contexts.input.isRightMousePress = false;
                
            }
        }
    }
}