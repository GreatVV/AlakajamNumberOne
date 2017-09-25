using System;

[Serializable]
public class RequiredIngredientDesc
{
    public IngredientsType Type;
    public ColorType Color;
    public int Amount;

    public override string ToString()
    {
        return string.Format("Type:{0} Color: {1} Amount: {2}", Type, Color, Amount);
    }
}