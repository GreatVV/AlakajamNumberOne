using Entitas;

[Game]
public class ColorComponent : IComponent
{
    public ColorType value;

    public override string ToString()
    {
        return value.ToString();
    }
}