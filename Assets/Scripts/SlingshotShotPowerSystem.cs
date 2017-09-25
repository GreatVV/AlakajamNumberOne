using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class SlingshotShotPowerSystem : ReactiveSystem<InputEntity>
{
    private Contexts _contexts;

    public SlingshotShotPowerSystem(Contexts contexts) : base(contexts.input)
    {
        _contexts = contexts;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Tick);
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var normalized = 0f;
        var slingshotEntity = _contexts.game.slingshotDescriptionEntity;
        if (_contexts.game.playerEntity.weapon.WeaponType == WeaponType.Slingshot)
        {
            if (_contexts.input.isRightMousePress)
            {
                var startPress = _contexts.input.rightMousePressEntity.time.value;
                var clickPress = Time.realtimeSinceStartup;
                var desc = _contexts.game.slingshotDescription.value;

                var newPower = Helper.GetPower(desc.MinPower, desc.MaxPower, clickPress - startPress,
                    desc.MaxPowerTime);

                _contexts.game.playerEntity.ReplaceShotPower(newPower);

                normalized = Mathf.Clamp01((clickPress - startPress) / desc.MaxPowerTime);
            }
        }
        var aim = slingshotEntity.aimView.value;
        aim.TargetTransform.localScale = Vector3.Lerp(aim.MaxSize, aim.MinSize, normalized);
            
    }
}