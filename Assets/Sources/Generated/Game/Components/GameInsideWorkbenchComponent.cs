//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly InsideWorkbenchComponent insideWorkbenchComponent = new InsideWorkbenchComponent();

    public bool isInsideWorkbench {
        get { return HasComponent(GameComponentsLookup.InsideWorkbench); }
        set {
            if (value != isInsideWorkbench) {
                if (value) {
                    AddComponent(GameComponentsLookup.InsideWorkbench, insideWorkbenchComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.InsideWorkbench);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherInsideWorkbench;

    public static Entitas.IMatcher<GameEntity> InsideWorkbench {
        get {
            if (_matcherInsideWorkbench == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.InsideWorkbench);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherInsideWorkbench = matcher;
            }

            return _matcherInsideWorkbench;
        }
    }
}
