//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AimViewComponent aimView { get { return (AimViewComponent)GetComponent(GameComponentsLookup.AimView); } }
    public bool hasAimView { get { return HasComponent(GameComponentsLookup.AimView); } }

    public void AddAimView(AimView newValue) {
        var index = GameComponentsLookup.AimView;
        var component = CreateComponent<AimViewComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAimView(AimView newValue) {
        var index = GameComponentsLookup.AimView;
        var component = CreateComponent<AimViewComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAimView() {
        RemoveComponent(GameComponentsLookup.AimView);
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

    static Entitas.IMatcher<GameEntity> _matcherAimView;

    public static Entitas.IMatcher<GameEntity> AimView {
        get {
            if (_matcherAimView == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AimView);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAimView = matcher;
            }

            return _matcherAimView;
        }
    }
}