//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity currentBossEntity { get { return GetGroup(GameMatcher.CurrentBoss).GetSingleEntity(); } }
    public CurrentBossComponent currentBoss { get { return currentBossEntity.currentBoss; } }
    public bool hasCurrentBoss { get { return currentBossEntity != null; } }

    public GameEntity SetCurrentBoss(Boss newValue) {
        if (hasCurrentBoss) {
            throw new Entitas.EntitasException("Could not set CurrentBoss!\n" + this + " already has an entity with CurrentBossComponent!",
                "You should check if the context already has a currentBossEntity before setting it or use context.ReplaceCurrentBoss().");
        }
        var entity = CreateEntity();
        entity.AddCurrentBoss(newValue);
        return entity;
    }

    public void ReplaceCurrentBoss(Boss newValue) {
        var entity = currentBossEntity;
        if (entity == null) {
            entity = SetCurrentBoss(newValue);
        } else {
            entity.ReplaceCurrentBoss(newValue);
        }
    }

    public void RemoveCurrentBoss() {
        currentBossEntity.Destroy();
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

    public CurrentBossComponent currentBoss { get { return (CurrentBossComponent)GetComponent(GameComponentsLookup.CurrentBoss); } }
    public bool hasCurrentBoss { get { return HasComponent(GameComponentsLookup.CurrentBoss); } }

    public void AddCurrentBoss(Boss newValue) {
        var index = GameComponentsLookup.CurrentBoss;
        var component = CreateComponent<CurrentBossComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCurrentBoss(Boss newValue) {
        var index = GameComponentsLookup.CurrentBoss;
        var component = CreateComponent<CurrentBossComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCurrentBoss() {
        RemoveComponent(GameComponentsLookup.CurrentBoss);
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

    static Entitas.IMatcher<GameEntity> _matcherCurrentBoss;

    public static Entitas.IMatcher<GameEntity> CurrentBoss {
        get {
            if (_matcherCurrentBoss == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CurrentBoss);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCurrentBoss = matcher;
            }

            return _matcherCurrentBoss;
        }
    }
}