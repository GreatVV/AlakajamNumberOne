using System.Text;

public static class RecipeExtensions
{
    public static string GetDescription(this Recipe recipe)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("Recipe of {0}\n", recipe.Name);
        stringBuilder.AppendFormat("You should use {0}\n", recipe.Liquid);
        stringBuilder.AppendLine("And then you should mix: ");
        foreach (var requiredIngredientDesc in recipe.Ingredients)
        {
            stringBuilder.AppendLine(requiredIngredientDesc.ToString());
        }
        return stringBuilder.ToString();
    } 
}