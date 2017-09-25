using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class CurrentBossComponent : IComponent
{
    public Boss value;
}