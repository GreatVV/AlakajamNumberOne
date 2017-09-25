using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class PlayerComponent : IComponent
{
    public PlayerDescription PlayerDescription;
}