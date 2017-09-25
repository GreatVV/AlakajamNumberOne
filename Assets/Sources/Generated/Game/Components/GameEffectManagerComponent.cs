//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity effectManagerEntity { get { return GetGroup(GameMatcher.EffectManager).GetSingleEntity(); } }
    public EffectManagerComponent effectManager { get { return effectManagerEntity.effectManager; } }
    public bool hasEffectManager { get { return effectManagerEntity != null; } }

    public GameEntity SetEffectManager(EffectManager newValue) {
        if (hasEffectManager) {
            throw new Entitas.EntitasException("Could not set EffectManager!\n" + this + " already has an entity with EffectManagerComponent!",
                "You should check if the context already has a effectManagerEntity before setting it or use context.ReplaceEffectManager().");
        }
        var entity = CreateEntity();
        entity.AddEffectManager(newValue);
        return entity;
    }

    public void ReplaceEffectManager(EffectManager newValue) {
        var entity = effectManagerEntity;
        if (entity == null) {
            entity = SetEffectManager(newValue);
        } else {
            entity.ReplaceEffectManager(newValue);
        }
    }

    public void RemoveEffectManager() {
        effectManagerEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public EffectManagerComponent effectManager { get { return (EffectManagerComponent)GetComponent(GameComponentsLookup.EffectManager); } }
    public bool hasEffectManager { get { return HasComponent(GameComponentsLookup.EffectManager); } }

    public void AddEffectManager(EffectManager newValue) {
        var index = GameComponentsLookup.EffectManager;
        var component = CreateComponent<EffectManagerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceEffectManager(EffectManager newValue) {
        var index = GameComponentsLookup.EffectManager;
        var component = CreateComponent<EffectManagerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveEffectManager() {
        RemoveComponent(GameComponentsLookup.EffectManager);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherEffectManager;

    public static Entitas.IMatcher<GameEntity> EffectManager {
        get {
            if (_matcherEffectManager == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.EffectManager);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEffectManager = matcher;
            }

            return _matcherEffectManager;
        }
    }
}