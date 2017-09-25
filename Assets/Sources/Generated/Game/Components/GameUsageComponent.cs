//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public UsageComponent usage { get { return (UsageComponent)GetComponent(GameComponentsLookup.Usage); } }
    public bool hasUsage { get { return HasComponent(GameComponentsLookup.Usage); } }

    public void AddUsage(UsageType newValue) {
        var index = GameComponentsLookup.Usage;
        var component = CreateComponent<UsageComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceUsage(UsageType newValue) {
        var index = GameComponentsLookup.Usage;
        var component = CreateComponent<UsageComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveUsage() {
        RemoveComponent(GameComponentsLookup.Usage);
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

    static Entitas.IMatcher<GameEntity> _matcherUsage;

    public static Entitas.IMatcher<GameEntity> Usage {
        get {
            if (_matcherUsage == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Usage);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUsage = matcher;
            }

            return _matcherUsage;
        }
    }
}