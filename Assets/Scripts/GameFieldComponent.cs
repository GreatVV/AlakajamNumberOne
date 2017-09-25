using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class GameFieldComponent : IComponent
{
    public int Width;
    public int Height;
}