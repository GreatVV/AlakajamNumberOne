//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public InventoryIconComponent inventoryIcon { get { return (InventoryIconComponent)GetComponent(GameComponentsLookup.InventoryIcon); } }
    public bool hasInventoryIcon { get { return HasComponent(GameComponentsLookup.InventoryIcon); } }

    public void AddInventoryIcon(InventoryIcon newValue) {
        var index = GameComponentsLookup.InventoryIcon;
        var component = CreateComponent<InventoryIconComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceInventoryIcon(InventoryIcon newValue) {
        var index = GameComponentsLookup.InventoryIcon;
        var component = CreateComponent<InventoryIconComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveInventoryIcon() {
        RemoveComponent(GameComponentsLookup.InventoryIcon);
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

    static Entitas.IMatcher<GameEntity> _matcherInventoryIcon;

    public static Entitas.IMatcher<GameEntity> InventoryIcon {
        get {
            if (_matcherInventoryIcon == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.InventoryIcon);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherInventoryIcon = matcher;
            }

            return _matcherInventoryIcon;
        }
    }
}
