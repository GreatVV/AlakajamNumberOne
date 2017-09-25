using Entitas;
using UnityEngine;

public class PauseGameSystem : IExecuteSystem
{
    private Contexts _contexts;

    public PauseGameSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _contexts.input.tickEntity.isPause = !_contexts.input.tickEntity.isPause;
            
            if (!_contexts.input.tickEntity.isPause)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}