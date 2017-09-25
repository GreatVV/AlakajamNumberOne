//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity rightMousePressEntity { get { return GetGroup(InputMatcher.RightMousePress).GetSingleEntity(); } }

    public bool isRightMousePress {
        get { return rightMousePressEntity != null; }
        set {
            var entity = rightMousePressEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isRightMousePress = true;
                } else {
                    entity.Destroy();
                }
            }
        }
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
public partial class InputEntity {

    static readonly RightMousePressComponent rightMousePressComponent = new RightMousePressComponent();

    public bool isRightMousePress {
        get { return HasComponent(InputComponentsLookup.RightMousePress); }
        set {
            if (value != isRightMousePress) {
                if (value) {
                    AddComponent(InputComponentsLookup.RightMousePress, rightMousePressComponent);
                } else {
                    RemoveComponent(InputComponentsLookup.RightMousePress);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherRightMousePress;

    public static Entitas.IMatcher<InputEntity> RightMousePress {
        get {
            if (_matcherRightMousePress == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.RightMousePress);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherRightMousePress = matcher;
            }

            return _matcherRightMousePress;
        }
    }
}