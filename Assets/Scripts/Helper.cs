using UnityEngine;
using System.Linq;

public static class Helper
{
    public static Vector3 RandomPosition(float width, float height, float fixedY = 1)
    {
        var randomX = Random.value;
        var randomZ = Random.value;
        return new Vector3(width * (randomX - 0.5f), fixedY, height * (randomZ - 0.5f));
    }

    public static bool Validate(Recipe recipe, GenerationDescription generationDescription)
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            var totalSum = generationDescription.Ingredients.Where(x => x.Color == ingredient.Color && x.Type == ingredient.Type).Sum(x => x.Amount);
            if (totalSum < ingredient.Amount)
            {
                Debug.LogWarningFormat("Problem with {0}. You need: {1} but have only: {2}", ingredient, ingredient.Amount, totalSum);
                return false;
            }
        }
        return true;
    }

    public static float GetPower(float minPower, float maxPower, float timePassed, float maxPowerTime)
    {
        return Mathf.Lerp(minPower, maxPower, timePassed / maxPowerTime);
    }

    public static long GetId(IngredientsType type, ColorType colorType)
    {
        return ((int)type << sizeof(IngredientsType)) | (int) colorType;
    }

    public static long GetId(this RequiredIngredientDesc requiredIngredientDesc)
    {
        return GetId(requiredIngredientDesc.Type, requiredIngredientDesc.Color);
    }
}