using Entitas;

[Game]
public class IngredientComponent : IComponent
{
    public IngredientsType value;

    public override string ToString()
    {
        return value.ToString();
    }
}